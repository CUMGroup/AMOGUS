using AMOGUS.Core.Domain.Models.ApiModels;

namespace AMOGUS.Core.Common.Interfaces.User
{
    public interface IAuthService
    {

        Task<LoginResultApiModel> RegisterUserAsync(string userName, string password, string role);

        Task<LoginResultApiModel> LoginUserAsync(string userName, string password);

        Task CreateRolesAsync<TRoles>();

    }
}
