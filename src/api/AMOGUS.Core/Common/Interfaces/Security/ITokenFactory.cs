
namespace AMOGUS.Core.Common.Interfaces.Security {
    public interface ITokenFactory {

        string GenerateBearerToken();
        Guid GenerateGuidToken();
        string GenerateHashedGuidToken();
        
    }
}
