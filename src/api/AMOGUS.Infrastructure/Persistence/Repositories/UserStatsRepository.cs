
using AMOGUS.Core.Common.Interfaces.Database;
using AMOGUS.Core.Common.Interfaces.Repositories;
using AMOGUS.Core.Domain.Models.Entities;
using HonkSharp.Fluency;
using Microsoft.EntityFrameworkCore;

namespace AMOGUS.Infrastructure.Persistence.Repositories {
    internal class UserStatsRepository : IUserStatsRepository {

        private readonly IApplicationDbContext _context;

        public UserStatsRepository(IApplicationDbContext context) {
            _context = context!;
        }

        public async Task<int> AddUserStatsAsync(UserStats userStats) {
            await _context.UserStats.AddAsync(userStats);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteUserStatsAsync(string userId) {
            var stats = await GetUserStatsAsync(userId);
            if (stats == null) {
                return 0;
            }
            _context.UserStats.Remove(stats);
            return await _context.SaveChangesAsync();
        }

        public Task<UserStats?> GetUserStatsAsync(string userId) {
            return _context.UserStats.Where(e => e.UserId.Equals(userId)).FirstOrDefaultAsync();
        }

        public Task<UserStats?> GetUserStatsIncludeUserAsync(string userId) {
            return _context.UserStats.Where(e => e.UserId.Equals(userId)).Include(e => e.User).FirstOrDefaultAsync();
        }

        public void RevertChanges(UserStats stats) {
            _context.RevertChanges(stats);
        }

        public async Task<int> UpdateUserStatsAsync(UserStats userStats) {
            _context.UserStats.Update(userStats);
            return await _context.SaveChangesAsync();
        }
    }
}
