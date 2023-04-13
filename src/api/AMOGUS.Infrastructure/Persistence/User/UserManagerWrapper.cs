
using AMOGUS.Core.Common.Interfaces.Database;
using AMOGUS.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace AMOGUS.Infrastructure.Persistence.User {
    internal class UserManagerWrapper : IUserManager {

        private readonly UserManager<ApplicationUser> _userManager;

        public UserManagerWrapper(UserManager<ApplicationUser> userManager) {
            _userManager = userManager;
        }

        public Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role) {
            return _userManager.AddToRoleAsync(user, role);
        }

        public Task<IdentityResult> AddToRolesAsync(ApplicationUser user, IEnumerable<string> roles) {
            return _userManager.AddToRolesAsync(user, roles);
        }

        public Task<bool> CheckPasswordAsync(ApplicationUser user, string password) {
            return _userManager.CheckPasswordAsync(user, password);
        }

        public Task<IdentityResult> CreateAsync(ApplicationUser user, string password) {
            return _userManager.CreateAsync(user, password);
        }

        public Task<IdentityResult> DeleteAsync(ApplicationUser user) {
            return _userManager.DeleteAsync(user);
        }

        public Task<ApplicationUser> FindByEmailAsync(string email) {
            return _userManager.FindByEmailAsync(email);
        }

        public Task<ApplicationUser> FindByIdAsync(string userId) {
            return _userManager.FindByIdAsync(userId);
        }

        public Task<ApplicationUser> FindByNameAsync(string userName) {
            return _userManager.FindByNameAsync(userName);
        }

        public Task<IList<string>> GetRolesAsync(ApplicationUser user) {
            return GetRolesAsync(user);
        }

        public Task<bool> IsInRoleAsync(ApplicationUser user, string role) {
            return IsInRoleAsync(user, role);
        }
    }
}
