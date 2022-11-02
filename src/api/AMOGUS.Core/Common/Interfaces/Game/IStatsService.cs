
using AMOGUS.Core.Domain.Models.Game;

namespace AMOGUS.Core.Common.Interfaces.Game {
    public interface IStatsService {

        Task<UserStats> GetUserStatsAsync(int userId);
        Task<bool> UpdateUserStatsAsync(UserStats userStats);
    }
}
