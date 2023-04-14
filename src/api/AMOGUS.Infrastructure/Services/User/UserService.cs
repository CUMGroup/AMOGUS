using AMOGUS.Core.Common.Communication;
using AMOGUS.Core.Common.Exceptions;
using AMOGUS.Core.Common.Interfaces.Database;
using AMOGUS.Core.Common.Interfaces.User;
using AMOGUS.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace AMOGUS.Infrastructure.Services.User {
    internal class UserService : IUserService {
        private readonly IUserManager _userManager;
        private readonly IApplicationDbContext _dbContext;

        public UserService(IUserManager userManager, IApplicationDbContext dbContext) {
            _userManager = userManager!;
            _dbContext = dbContext!;
        }

        public async Task<Result> DeleteUserAsync(string userId) {
            if (await DeleteUserHistoryAsync(userId)) {
                return new UserOperationException("Failed deleting user.");
            }

            var userRes = await GetUserAsync(userId);
            if (userRes.IsFaulted) {
                return userRes;
            }
            IdentityResult result = await _userManager.DeleteAsync(userRes.Value);
            if (!result.Succeeded) {
                return new UserOperationException(String.Join(';', result.Errors.Select(e => e.Code + ": " + e.Description)));
            }
            return true;
        }

        private async Task<bool> DeleteUserHistoryAsync(string userId) {
            var medalsToDelete = _dbContext.UserMedals.Where(m => m.UserId.Equals(userId)).ToList();
            _dbContext.UserMedals.RemoveRange(medalsToDelete);

            var statsToDelete = _dbContext.UserStats.Where(us => us.UserId.Equals(userId)).ToList();
            _dbContext.UserStats.RemoveRange(statsToDelete);

            var sessionsToDelete = _dbContext.GameSessions.Where(gs => gs.UserId.Equals(userId)).ToList();
            _dbContext.GameSessions.RemoveRange(sessionsToDelete);

            try {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex) {
                return false;
            }
            return true;
        }

        public async Task<Result<ApplicationUser>> GetUserAsync(string userId) {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            if (user == null) {
                return new UserNotFoundException($"The user with the userID {userId} couldn't be found qwq.");
            }
            return user;
        }

        public async Task<bool> IsInRoleAsync(string userId, string role) {
            var userRes = await GetUserAsync(userId);

            return await userRes.Match(
                user => _userManager.IsInRoleAsync(user, role),
                err => Task.FromResult(false)
            );
        }
    }
}
