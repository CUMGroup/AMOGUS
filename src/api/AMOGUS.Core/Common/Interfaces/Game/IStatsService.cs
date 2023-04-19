using AMOGUS.Core.Common.Communication;
using AMOGUS.Core.DataTransferObjects.User;
using AMOGUS.Core.Domain.Models.Entities;
using AMOGUS.Infrastructure.Identity;

namespace AMOGUS.Core.Common.Interfaces.Game {
    public interface IStatsService {

        Task<Result<UserStats>> GetUserStatsAsync(string userId);

        Task<bool> UpdateUserStatsAsync(UserStats userStats);

        Task<bool> UpdateUserStatsAsync(GameSession session, bool[] answers, ApplicationUser user);

        Task<Result<UserStatsApiModel>> GetDetailedUserStatsModel(string userId);
    }
}
