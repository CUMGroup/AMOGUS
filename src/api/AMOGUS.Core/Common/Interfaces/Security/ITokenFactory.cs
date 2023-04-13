using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AMOGUS.Infrastructure.Identity;

namespace AMOGUS.Core.Common.Interfaces.Security {
    public interface ITokenFactory {

        Guid GenerateGuidToken();

        string GenerateHashedGuidToken();

        Task<List<Claim>> GetUserAuthClaimsFromRolesAsync(IList<string> userRoles, ApplicationUser user);

        JwtSecurityToken GenerateNewJwtSecurityToken(List<Claim> authClaims);
    }
}
