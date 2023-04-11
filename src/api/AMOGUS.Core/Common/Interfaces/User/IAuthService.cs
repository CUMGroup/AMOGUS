using AMOGUS.Core.DataTransferObjects.User;

namespace AMOGUS.Core.Common.Interfaces.User {
    public interface IAuthService {

        Task<LoginResultApiModel> RegisterUserAsync(RegisterApiModel registerModel, string role);

        Task<LoginResultApiModel> LoginUserAsync(LoginApiModel loginModel);

        Task CreateRolesAsync<TRoles>();
    }
}
