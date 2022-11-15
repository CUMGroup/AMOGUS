using AMOGUS.Core.Common.Interfaces.Database;
using AMOGUS.Core.Domain.Models.Entities;
using AMOGUS.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMOGUS.Infrastructure.Persistence {
    internal class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext {

        public DbSet<UserStats> UserStats { get; set; }
        public DbSet<UserMedal> UserMedals { get; set; }
        public DbSet<GameSession> GameSessions { get; set; }
        public DbSet<Question> Exercises { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {

        }

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
    }
}
