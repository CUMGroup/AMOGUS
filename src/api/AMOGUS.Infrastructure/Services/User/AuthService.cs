using AMOGUS.Core.Common.Communication;
using AMOGUS.Core.Common.Exceptions;
using AMOGUS.Core.Common.Interfaces.Database;
using AMOGUS.Core.Common.Interfaces.Repositories;
using AMOGUS.Core.Common.Interfaces.Security;
using AMOGUS.Core.Common.Interfaces.User;
using AMOGUS.Core.DataTransferObjects.User;
using AMOGUS.Core.Domain.Models.Entities;
using AMOGUS.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;

namespace AMOGUS.Infrastructure.Services.User {
    internal class AuthService : IAuthService {

        private readonly ITokenFactory _tokenFactory;
        private readonly IRoleManager _roleManager;
        private readonly IUserManager _userManager;

        private readonly IUserStatsRepository _userStatsRepository;

        public AuthService(IRoleManager _roleManager, IUserManager _userManager, ITokenFactory _tokenFactory, IUserStatsRepository userStatsRepository) {
            this._roleManager = _roleManager!;
            this._userManager = _userManager!;
            this._tokenFactory = _tokenFactory!;
            _userStatsRepository = userStatsRepository!;
        }

        public async Task CreateRolesAsync<TRoles>() {
            var roles = typeof(TRoles).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(f => f.IsLiteral && !f.IsInitOnly && (f.FieldType == typeof(string) || f.FieldType == typeof(String)) && f.GetRawConstantValue() != null)
                .Select(x => (string) (x.GetRawConstantValue() ?? ""))
                .ToList();

            foreach (string r in roles) {
                if (!(await _roleManager.RoleExistsAsync(r))) {
                    await _roleManager.CreateAsync(new IdentityRole(r));
                }
            }
        }

        public async Task<Result<LoginResultApiModel>> LoginUserAsync(LoginApiModel loginModel) {

            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginModel.Password)) {
                return new AuthFailureException("Invalid Username or Password!");
            }
            var userRoles = await _userManager.GetRolesAsync(user);
            List<Claim> authClaims = _tokenFactory.GetUserAuthClaimsFromRoles(userRoles, user);
            JwtSecurityToken token = _tokenFactory.GenerateNewJwtSecurityToken(authClaims);

            return new LoginResultApiModel(new JwtSecurityTokenHandler().WriteToken(token), token.ValidTo, user.UserName, user.Email);
        }

        public async Task<Result<LoginResultApiModel>> RegisterUserAsync(RegisterApiModel registerModel, string role) {

            var userExists = await _userManager.FindByEmailAsync(registerModel.Email);
            if (userExists != null) {
                return new AuthFailureException("Account already exists!");
            }

            ApplicationUser user = CreateNewApplicationUserModel(registerModel);

            var result = await _userManager.CreateAsync(user, registerModel.Password);
            if (!result.Succeeded)
                return new AuthFailureException("Failed to create Account");

            if (!await _roleManager.RoleExistsAsync(role)) {
                return new AuthFailureException("Failed to create Account");
            }

            await _userManager.AddToRoleAsync(user, role);

            var res = await CreateNewUserStatsAsync(user);
            if (res.IsFaulted)
                return new Exception("Did not finish creating the user", res);

            return await LoginUserAsync(new LoginApiModel { Email = registerModel.Email, Password = registerModel.Password });
        }

        private static ApplicationUser CreateNewApplicationUserModel(RegisterApiModel registerModel) {
            return new() {
                Email = registerModel.Email,
                UserName = registerModel.UserName,
                SecurityStamp = Guid.NewGuid().ToString()
            };
        }

        private async Task<Result<UserStats>> CreateNewUserStatsAsync(ApplicationUser user) {
            var stats = new UserStats {
                User = user,
                UserId = user.Id
            };
            var res = await _userStatsRepository.AddUserStatsAsync(stats);
            return res > 0 ? stats : new UserOperationException("Could not create stats object");
        }
    }
}
