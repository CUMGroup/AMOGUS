using AMOGUS.Core.Common.Interfaces.Game;
using AMOGUS.Core.Common.Interfaces.Repositories;
using AMOGUS.Core.DataTransferObjects.User;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AMOGUS.UnitTests")]
namespace AMOGUS.Core.Services.Information {
    internal class LeaderboardService : ILeaderboardService {

        private readonly IUserStatsRepository _userStatsRepository;

        public LeaderboardService(IUserStatsRepository userStatsRepository) {
            _userStatsRepository = userStatsRepository;
        }

        public async Task<LeaderboardApiModel> GetLeaderboardAsync() {
            var longest = await _userStatsRepository.GetTopOrderedByAsync(5, e => e.LongestStreak);
            var current = await _userStatsRepository.GetTopOrderedByAsync(5, e => e.CurrentStreak);
            var correctRatio = await _userStatsRepository.GetTopOrderedByAsync(5, e => e.GetCorrectRatio());

            return new LeaderboardApiModel(
                LongestStreaks: longest.Select(x =>
                    new LeaderboardUserStreak {
                        Username = x.User?.UserName ?? string.Empty,
                        Streak = x.LongestStreak
                    }).ToArray(),
                CurrentStreaks: current.Select(x =>
                    new LeaderboardUserStreak {
                        Username = x.User?.UserName ?? string.Empty,
                        Streak = x.CurrentStreak
                    }
                ).ToArray(),
                CorrectRatios: correctRatio.Select(x =>
                    new LeaderboardUserCorrectRatio {
                        Username = x.User?.UserName ?? string.Empty,
                        CorrectRatio = x.GetCorrectRatio()
                    }
                ).ToArray()
            );
        }
    }
}
