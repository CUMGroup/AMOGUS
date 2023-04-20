using AMOGUS.Core.Common.Communication;
using AMOGUS.Core.DataTransferObjects.User;

namespace AMOGUS.Core.Common.Interfaces.User {
    public interface IAuthService {

        Task<Result<LoginResultApiModel>> RegisterUserAsync(RegisterApiModel registerModel, string role);

        Task<Result<LoginResultApiModel>> LoginUserAsync(LoginApiModel loginModel);

        Task CreateRolesAsync<TRoles>();
    }
}
