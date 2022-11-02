
using AMOGUS.Core.Common.Communication;

namespace AMOGUS.Core.Common.Interfaces.Game {
    public interface IStreakService {

        Task UpdateAllStreaks();
        Task<int> ReadStreak(string userId);
    }
}
