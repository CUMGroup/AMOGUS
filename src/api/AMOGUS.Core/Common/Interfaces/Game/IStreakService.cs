
using AMOGUS.Core.Common.Communication;

namespace AMOGUS.Core.Common.Interfaces.Game {
    public interface IStreakService {

        Task UpdateAllStreaksAsync();
        Task<int> ReadStreakAsync(string userId);
    }
}
