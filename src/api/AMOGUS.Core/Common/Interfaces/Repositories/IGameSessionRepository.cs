
using AMOGUS.Core.Domain.Models.Entities;

namespace AMOGUS.Core.Common.Interfaces.Repositories {
    public interface IGameSessionRepository {

        Task<int> DeleteGameSessionsByUserIdAsync(string userId);

        Task<int> AddGameSessionAsync(GameSession session);

        Task<List<GameSession>> GetAllByUserIdAsync(string userId);

    }
}
