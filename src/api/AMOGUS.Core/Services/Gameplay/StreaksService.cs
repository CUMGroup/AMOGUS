using AMOGUS.Core.Common.Communication;
using AMOGUS.Core.Common.Interfaces.Database;
using AMOGUS.Core.Common.Interfaces.Game;
using AMOGUS.Core.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace AMOGUS.Core.Services.Gameplay {
    internal class StreaksService : IStreakService {

        private readonly IStatsService _statsService;
        private readonly IUserManager _userManager;

        public StreaksService(IStatsService statsService, IUserManager userManager) {
            _statsService = statsService!;
            _userManager = userManager!;
        }

        public async Task<Result<int>> ReadStreakAsync(string userId) {
            var res = await _statsService.GetUserStatsAsync(userId);
            return res.Map(e => e.CurrentStreak);
        }

        public async Task UpdateAllStreaksAsync() {
            var allUsers = await _userManager.GetAllAsync();

            foreach(var user in allUsers) {
                var statsResult = await _statsService.GetUserStatsAsync(user.Id);
                if (statsResult.IsFaulted)
                    continue;
                var stats = statsResult.Value;

                UpdateStatsModel(stats, user.PlayedToday);
                stats.User.PlayedToday = false;

                await _statsService.UpdateUserStatsAsync(stats);
            }
        }

        private void UpdateStatsModel(UserStats stats, bool playedToday) {
            if (playedToday) {
                stats.CurrentStreak += 1;
                if (stats.CurrentStreak > stats.LongestStreak) {
                    stats.LongestStreak = stats.CurrentStreak;
                }
            }
            else {
                stats.CurrentStreak = 0;
            }
        }
    }
}
