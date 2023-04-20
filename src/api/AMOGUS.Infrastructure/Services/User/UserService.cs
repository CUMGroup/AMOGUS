using AMOGUS.Core.Common.Communication;
using AMOGUS.Core.Common.Exceptions;
using AMOGUS.Core.Common.Interfaces.Database;
using AMOGUS.Core.Common.Interfaces.Repositories;
using AMOGUS.Core.Common.Interfaces.User;
using AMOGUS.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace AMOGUS.Infrastructure.Services.User {
    internal class UserService : IUserService {
        private readonly IUserManager _userManager;

        private readonly IUserMedalRepository _userMedalRepository;
        private readonly IGameSessionRepository _gameSessionRepository;
        private readonly IUserStatsRepository _userStatsRepository;

        public UserService(IUserManager userManager, IUserMedalRepository userMedalRepository, IGameSessionRepository gameSessionRepository, IUserStatsRepository userStatsRepository) {
            _userManager = userManager!;
            _userMedalRepository = userMedalRepository!;
            _gameSessionRepository = gameSessionRepository!;
            _userStatsRepository = userStatsRepository!;
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
            try {
                await _userMedalRepository.DeleteUserMedalsByUserIdAsync(userId);

                await _userStatsRepository.DeleteUserStatsAsync(userId);

                await _gameSessionRepository.DeleteGameSessionsByUserIdAsync(userId);
            }
            catch (Exception) {
                return false;
            }
            return true;
        }

        public async Task<Result<ApplicationUser>> GetUserAsync(string userId) {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            if (user == null) {
                return new RecordNotFoundException($"The user with the userID {userId} couldn't be found qwq.");
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
