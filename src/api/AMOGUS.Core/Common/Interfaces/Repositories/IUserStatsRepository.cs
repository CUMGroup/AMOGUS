
using AMOGUS.Core.Domain.Models.Entities;
using System.Linq.Expressions;

namespace AMOGUS.Core.Common.Interfaces.Repositories {
    public interface IUserStatsRepository {

        Task<int> AddUserStatsAsync(UserStats userStats);

        Task<int> DeleteUserStatsAsync(string userId);

        Task<int> UpdateUserStatsAsync(UserStats userStats);

        Task<UserStats?> GetUserStatsIncludeUserAsync(string userId);

        Task<UserStats?> GetUserStatsAsync(string userId);

        Task<List<UserStats>> GetTopOrderedByAsync<TKey>(int amount, Expression<Func<UserStats, TKey>> orderExpr);

        void RevertChanges(UserStats stats);
    }
}
