using AMOGUS.Core.Common.Communication;
using AMOGUS.Core.Common.Exceptions;
using AMOGUS.Core.Common.Interfaces.Database;
using AMOGUS.Core.Common.Interfaces.User;
using AMOGUS.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AMOGUS.Infrastructure.Services.User {
    internal class UserService : IUserService {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationDbContext _dbContext;

        public UserService(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IApplicationDbContext dbContext) {
            _roleManager = roleManager;
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async Task<Result> DeleteUserAsync(string userId) {
            var medalsToDelete = _dbContext.UserMedals.Where(m => m.UserId.Equals(userId)).ToList();
            _dbContext.UserMedals.RemoveRange(medalsToDelete);

            var statsToDelete = _dbContext.UserStats.Where(us => us.UserId.Equals(userId)).ToList();
            _dbContext.UserStats.RemoveRange(statsToDelete);

            var sessionsToDelete = _dbContext.GameSessions.Where(gs => gs.UserId.Equals(userId)).ToList();
            _dbContext.GameSessions.RemoveRange(sessionsToDelete);

            try {
                await _dbContext.SaveChangesAsync();
            } catch (Exception ex) {
                return Result.Failure("Failed deleting user.");
            }

            ApplicationUser user = await GetUserAsync(userId);
            IdentityResult result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded) {
                return Result.Failure(result.Errors.Select(e => (e.Code + ": " + e.Description)));
            }
            return Result.Success();
        }

        public async Task<ApplicationUser> GetUserAsync(string userId) {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            if (user == null) {
                throw new UserNotFoundException($"The user with the userID {userId} coudn't be found qwq.");
            }
            return user;
        }

        public async Task<bool> IsInRoleAsync(string userId, string role) {
            ApplicationUser user = await GetUserAsync(userId);

            return await _userManager.IsInRoleAsync(user, role);
        }
    }
}
