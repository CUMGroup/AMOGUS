using AMOGUS.Core.Common.Interfaces.Configuration;
using AMOGUS.Core.Common.Interfaces.Security;
using AMOGUS.Infrastructure.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

[assembly: InternalsVisibleTo("AMOGUS.UnitTests")]
namespace AMOGUS.Infrastructure.Services.User {
    [ExcludeFromCodeCoverage]
    internal class TokenFactory : ITokenFactory {

        private readonly IJwtConfiguration _jwtConfiguration;

        public TokenFactory(IJwtConfiguration jwtConfiguration) {
            _jwtConfiguration = jwtConfiguration!;
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
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.Key));

            return new JwtSecurityToken(
                            issuer: _jwtConfiguration.Issuer,
                            audience: _jwtConfiguration.Audience,
                            expires: DateTime.Now.AddHours(12),
                            claims: authClaims,
                            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                            );
        }

        public List<Claim> GetUserAuthClaimsFromRoles(IList<string> userRoles, ApplicationUser user) {
            var authClaims = new List<Claim> {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles) {
                authClaims.Add(new Claim("roles", userRole));
            }

            return authClaims;
        }
    }
}
