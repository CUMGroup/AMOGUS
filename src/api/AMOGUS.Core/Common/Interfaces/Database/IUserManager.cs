
using AMOGUS.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace AMOGUS.Core.Common.Interfaces.Database {
    public interface IUserManager {

        Task<IdentityResult> CreateAsync(ApplicationUser user, string password);

        Task<IdentityResult> DeleteAsync(ApplicationUser user);

        Task<ApplicationUser> FindByIdAsync(string userId);

        Task<ApplicationUser> FindByNameAsync(string userName);

        Task<ApplicationUser> FindByEmailAsync(string email);

        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);

        Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role);

        Task<IdentityResult> AddToRolesAsync(ApplicationUser user, IEnumerable<string> roles);

        Task<IList<string>> GetRolesAsync(ApplicationUser user);

        Task<bool> IsInRoleAsync(ApplicationUser user, string role);
    }
}
