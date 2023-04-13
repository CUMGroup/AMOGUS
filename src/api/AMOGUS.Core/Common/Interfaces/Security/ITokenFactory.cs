using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AMOGUS.Infrastructure.Identity;

namespace AMOGUS.Core.Common.Interfaces.Security {
    public interface ITokenFactory {

        Guid GenerateGuidToken();

        string GenerateHashedGuidToken();

        List<Claim> GetUserAuthClaimsFromRoles(IList<string> userRoles, ApplicationUser user);

        JwtSecurityToken GenerateNewJwtSecurityToken(List<Claim> authClaims);
    }
}
