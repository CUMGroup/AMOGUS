using AMOGUS.Core.Common.Communication;
using AMOGUS.Infrastructure.Identity;

namespace AMOGUS.Core.Common.Interfaces.User {
    public interface IUserService {

        Task<ApplicationUser> GetUserAsync(string userId);

        Task<bool> IsInRoleAsync(string userId, string role);

        Task<Result> DeleteUserAsync(string userId);
    }
}
