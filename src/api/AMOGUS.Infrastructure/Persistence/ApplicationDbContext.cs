using AMOGUS.Core.Common.Interfaces.Database;
using AMOGUS.Core.Domain.Models.Entities;
using AMOGUS.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AMOGUS.Infrastructure.Persistence {
    internal class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext {

        public DbSet<UserStats> UserStats { get; set; }

        public DbSet<UserMedal> UserMedals { get; set; }

        public DbSet<GameSession> GameSessions { get; set; }


#pragma warning disable 8618  // Fields cannot be null after constructor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
        }
#pragma warning restore 8618
         
        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            builder.Entity<UserMedal>().HasKey(u => new { u.MedalId, u.UserId });
        }


        public async Task<bool> EnsureDatabaseAsync() {
            return await base.Database.EnsureCreatedAsync();
        }

        public async Task MigrateDatabaseAsync() {
            await base.Database.MigrateAsync();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken) {
            return await base.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> SaveChangesAsync() {
            return await base.SaveChangesAsync();
        }

        public void RevertChanges<TEntity>(TEntity entity) where TEntity : class {
            try {
                var entry = Entry<TEntity>(entity);
                switch (entry.State) {
                    case EntityState.Modified:
                        entry.CurrentValues.SetValues(entry.OriginalValues);
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;
                }
            }catch(Exception) { }
        }
    }
}
