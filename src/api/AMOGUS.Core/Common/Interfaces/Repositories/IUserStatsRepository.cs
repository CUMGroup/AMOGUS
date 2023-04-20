﻿
using AMOGUS.Core.Domain.Models.Entities;

namespace AMOGUS.Core.Common.Interfaces.Repositories {
    public interface IUserStatsRepository {

        Task<int> DeleteUserStatsAsync(string userId);

        Task<int> UpdateUserStatsAsync(UserStats userStats);

        Task<UserStats?> GetUserStatsIncludeUserAsync(string userId);

        Task<UserStats?> GetUserStatsAsync(string userId);
    }
}
