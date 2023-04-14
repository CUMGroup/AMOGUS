
using AMOGUS.Core.Common.Interfaces.Database;
using Microsoft.AspNetCore.Identity;

namespace AMOGUS.Infrastructure.Persistence.User {
    internal class RoleManagerWrapper : IRoleManager {

        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleManagerWrapper(RoleManager<IdentityRole> roleManager) {
            _roleManager = roleManager!;
        }

        public Task<IdentityResult> CreateAsync(IdentityRole role) {
            return _roleManager.CreateAsync(role);
        }

        public Task<bool> RoleExistsAsync(string roleName) {
            return _roleManager.RoleExistsAsync(roleName);
        }
    }
}
