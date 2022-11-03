using AMOGUS.Core.Common.Communication;

namespace AMOGUS.Core.Common.Interfaces.User
{
    public interface IUserService
    {

        Task<IApplicationUser> GetUserAsync(string userId);

        Task<bool> IsInRoleAsync(string userId, string role);

        Task<Result> DeleteUserAsync(string userId);

    }
}
