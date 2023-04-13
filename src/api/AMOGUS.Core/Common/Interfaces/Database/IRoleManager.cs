
using Microsoft.AspNetCore.Identity;

namespace AMOGUS.Core.Common.Interfaces.Database {
    public interface IRoleManager {

        Task<IdentityResult> CreateAsync(IdentityRole role);

        Task<bool> RoleExistsAsync(string roleName);


    }
}
