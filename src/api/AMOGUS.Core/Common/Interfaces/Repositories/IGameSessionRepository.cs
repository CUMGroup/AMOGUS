
using AMOGUS.Core.Domain.Models.Entities;
using System.Linq.Expressions;

namespace AMOGUS.Core.Common.Interfaces.Repositories {
    public interface IGameSessionRepository {

        Task<int> DeleteGameSessionsByUserIdAsync(string userId);

        Task<int> AddGameSessionAsync(GameSession session);

        Task<List<GameSession>> GetAllByUserIdAsync(string userId);

        Task<List<GameSession>> GetAllBy(Expression<Func<GameSession, bool>> predicate);

    }
}
