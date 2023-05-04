using AMOGUS.Core.Domain.Models.Entities;
using AMOGUS.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AMOGUS.Core.Common.Interfaces.Database {
    public interface IApplicationDbContext {
        public DbSet<UserStats> UserStats { get; set; }

        public DbSet<UserMedal> UserMedals { get; set; }

        public DbSet<GameSession> GameSessions { get; set; }

        public DbSet<ApplicationUser> Users { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        Task<int> SaveChangesAsync();

        Task<bool> EnsureDatabaseAsync();

        Task MigrateDatabaseAsync();

        void RevertChanges<TEntity>(TEntity entity) where TEntity : class;



    }
}
