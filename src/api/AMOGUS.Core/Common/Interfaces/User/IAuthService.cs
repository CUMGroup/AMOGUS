using AMOGUS.Core.Domain.Models.ApiModels;

namespace AMOGUS.Core.Common.Interfaces.User
{
    public interface IAuthService
    {

        Task<LoginResultApiModel> RegisterUserAsync(RegisterApiModel registerModel, string role);

        Task<LoginResultApiModel> LoginUserAsync(LoginApiModel loginModel);

        Task CreateRolesAsync<TRoles>();

    }
}
