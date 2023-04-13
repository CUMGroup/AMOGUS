using AMOGUS.Core.Common.Communication;
using AMOGUS.Core.Common.Interfaces.User;
using AMOGUS.Core.DataTransferObjects.User;
using AMOGUS.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;

namespace AMOGUS.Infrastructure.Services.User {
    internal class AuthService : IAuthService {

        private readonly TokenFactory _tokenFactory;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthService(RoleManager<IdentityRole> _roleManager, UserManager<ApplicationUser> _userManager, TokenFactory _tokenFactory) {
            this._roleManager = _roleManager!;
            this._userManager = _userManager!;
            this._tokenFactory = _tokenFactory!;
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

        public async Task<LoginResultApiModel> LoginUserAsync(LoginApiModel loginModel) {

            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginModel.Password)) {
                return new LoginResultApiModel();
            }
            var userRoles = await _userManager.GetRolesAsync(user);
            List<Claim> authClaims = await _tokenFactory.GetUserAuthClaimsFromRolesAsync(userRoles, user);
            JwtSecurityToken token = _tokenFactory.GenerateNewJwtSecurityToken(authClaims);

            return new LoginResultApiModel(Result.Success(), new JwtSecurityTokenHandler().WriteToken(token), token.ValidTo, user.UserName, user.Email);
        }

        public async Task<LoginResultApiModel> RegisterUserAsync(RegisterApiModel registerModel, string role) {

            var userExists = await _userManager.FindByEmailAsync(registerModel.Email);
            if (userExists != null) {
                return new LoginResultApiModel(Result.Failure("Account already exists!"));
            }

            ApplicationUser user = CreateNewApplicationUserModel(registerModel);

            var result = await _userManager.CreateAsync(user, registerModel.Password);
            if (!result.Succeeded)
                return new LoginResultApiModel(Result.Failure("Failed to create Account"));

            if (!await _roleManager.RoleExistsAsync(role)) {
                return new LoginResultApiModel(Result.Failure("Failed to create Account"));
            }

            await _userManager.AddToRoleAsync(user, role);

            return await LoginUserAsync(new LoginApiModel { Email = registerModel.Email, Password = registerModel.Password });
        }

        private static ApplicationUser CreateNewApplicationUserModel(RegisterApiModel registerModel) {
            return new() {
                Email = registerModel.Email,
                UserName = registerModel.UserName,
                SecurityStamp = Guid.NewGuid().ToString()
            };
        }
    }
}
