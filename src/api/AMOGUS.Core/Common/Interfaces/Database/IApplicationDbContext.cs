using AMOGUS.Core.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AMOGUS.Core.Common.Interfaces.Database
{
    public interface IApplicationDbContext
    {
        public DbSet<UserStats> UserStats { get; set; }
        public DbSet<UserMedal> UserMedals { get; set; }
        public DbSet<GameSession> GameSessions { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync();

        Task<bool> EnsureDatabaseAsync();
        Task MigrateDatabaseAsync();

    }
}
