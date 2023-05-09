
using AMOGUS.Core.DataTransferObjects.User;

namespace AMOGUS.Core.Common.Interfaces.Game {
    public interface ILeaderboardService {

        Task<LeaderboardApiModel> GetLeaderboardAsync();

    }
}
