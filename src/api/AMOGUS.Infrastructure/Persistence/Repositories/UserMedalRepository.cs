using AMOGUS.Core.Common.Interfaces.Database;
using AMOGUS.Core.Common.Interfaces.Repositories;
using AMOGUS.Core.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AMOGUS.Infrastructure.Persistence.Repositories {
    internal class UserMedalRepository : IUserMedalRepository {

        private readonly IApplicationDbContext _context;

        public UserMedalRepository(IApplicationDbContext context) {
            _context = context!;
        }

        public async Task<int> DeleteUserMedalsByUserIdAsync(string userId) {
            var medals = await GetUserMedalsByUserIdAsync(userId);
            _context.UserMedals.RemoveRange(medals);
            return await _context.SaveChangesAsync();
        }

        public Task<List<UserMedal>> GetUserMedalsByUserIdAsync(string userId) {
            return _context.UserMedals.Where(e => e.UserId.Equals(userId)).ToListAsync();
        }
    }
}
