using AMOGUS.Core.Common.Interfaces.Database;
using AMOGUS.Core.Common.Interfaces.Repositories;
using AMOGUS.Core.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AMOGUS.Infrastructure.Persistence.Repositories {
    internal class GameSessionRepository : IGameSessionRepository {

        private readonly IApplicationDbContext _context;

        public GameSessionRepository(IApplicationDbContext context) {
            _context = context!;
        }

        public async Task<int> AddGameSessionAsync(GameSession session) {
            await _context.GameSessions.AddAsync(session);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteGameSessionsByUserIdAsync(string userId) {
            var sessions = await GetAllByUserIdAsync(userId);
            _context.GameSessions.RemoveRange(sessions);
            return await _context.SaveChangesAsync();
        }

        public Task<List<GameSession>> GetAllBy(Expression<Func<GameSession, bool>> predicate) {
            return _context.GameSessions.Where(predicate).ToListAsync();
        }

        public Task<List<GameSession>> GetAllByUserIdAsync(string userId) {
            return _context.GameSessions.Where(e => e.UserId.Equals(userId)).ToListAsync();
        }
    }
}
