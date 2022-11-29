using AMOGUS.Core.Common.Communication;
using AMOGUS.Core.Common.Interfaces.User;
using AMOGUS.Core.Domain.Models.ApiModels;
using AMOGUS.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AMOGUS.Infrastructure.Services.User {
    internal class AuthService : IAuthService {

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthService(RoleManager<IdentityRole> _roleManager, UserManager<ApplicationUser> _userManager, IConfiguration _configuration) {
            this._roleManager = _roleManager;
            this._userManager = _userManager;
            this._configuration = _configuration;
        }

        public async Task CreateRolesAsync<TRoles>() {
            var roles = typeof(TRoles).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(f => f.IsLiteral && !f.IsInitOnly && (f.FieldType == typeof(string) || f.FieldType == typeof(String)) && f.GetRawConstantValue() != null)
                .Select(x => (string)(x.GetRawConstantValue() ?? ""))
                .ToList();

            foreach (string r in roles) {
                if (!(await _roleManager.RoleExistsAsync(r))) {
                    await _roleManager.CreateAsync(new IdentityRole(r));
                }
            }
        }

        public async Task<LoginResultApiModel> LoginUserAsync(LoginApiModel loginModel) {

            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginModel.Password))
                return new LoginResultApiModel();

            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim> {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach(var userRole in userRoles) {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddHours(12),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return new LoginResultApiModel(Result.Success(), new JwtSecurityTokenHandler().WriteToken(token), token.ValidTo, user.UserName, user.Email);
        }


        public async Task<LoginResultApiModel> RegisterUserAsync(RegisterApiModel registerModel, string role) {

            var userExists = await _userManager.FindByEmailAsync(registerModel.Email);
            if(userExists != null) {
                return new LoginResultApiModel(Result.Failure("Account already exists!"));
            }

            ApplicationUser user = new() {
                Email = registerModel.Email,
                UserName = registerModel.UserName,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            
            var result = await _userManager.CreateAsync(user, registerModel.Password);
            if (!result.Succeeded)
                return new LoginResultApiModel(Result.Failure("Failed to create Account"));

            if(!await _roleManager.RoleExistsAsync(role)) {
                return new LoginResultApiModel(Result.Failure("Failed to create Account"));
            }

            await _userManager.AddToRoleAsync(user, role);

            return await LoginUserAsync(new LoginApiModel { Email = registerModel.Email, Password = registerModel.Password});
        }
    }
}
