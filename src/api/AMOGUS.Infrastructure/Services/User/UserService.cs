using AMOGUS.Core.Common.Communication;
using AMOGUS.Core.Common.Interfaces.User;
using AMOGUS.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMOGUS.Infrastructure.Services.User {
    internal class UserService : IUserService {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager) {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public Task<Result> DeleteUserAsync(string userId) {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> GetUserAsync(string userId) {
            throw new NotImplementedException();
        }

        public Task<bool> IsInRoleAsync(string userId, string role) {
            throw new NotImplementedException();
        }
    }
}
