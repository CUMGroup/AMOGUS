using AMOGUS.Core.Common.Interfaces.Security;
using AMOGUS.Infrastructure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AMOGUS.Infrastructure.Services.User {
    internal class TokenFactory : ITokenFactory {

        private readonly IConfiguration _configuration;

        public TokenFactory(IConfiguration configuration) {
            this._configuration = configuration;
        }

        public Guid GenerateGuidToken() {
            return Guid.NewGuid();
        }

        public string GenerateHashedGuidToken() {
            var guid = GenerateGuidToken();
            using (var hashAlg = SHA256.Create()) {
                byte[] crypt = hashAlg.ComputeHash(Encoding.UTF8.GetBytes(guid.ToString()));
                return Convert.ToBase64String(crypt);
            }
        }

        public JwtSecurityToken GenerateNewJwtSecurityToken(List<Claim> authClaims) {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            return new JwtSecurityToken(
                            issuer: _configuration["Jwt:Issuer"],
                            audience: _configuration["Jwt:Audience"],
                            expires: DateTime.Now.AddHours(12),
                            claims: authClaims,
                            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                            );
        }

        public async Task<List<Claim>> GetUserAuthClaimsFromRolesAsync(IList<string> userRoles, ApplicationUser user) {
            var authClaims = new List<Claim> {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles) {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            return authClaims;
        }
    }
}
