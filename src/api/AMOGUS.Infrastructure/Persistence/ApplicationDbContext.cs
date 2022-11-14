using AMOGUS.Core.Common.Interfaces.Database;
using AMOGUS.Core.Domain.Models.Game;
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
        public DbSet<UserMedals> UserMedals { get; set; }
        public DbSet<GameSession> GameSessions { get; set; }
        public DbSet<Exercise> Exercises { get; set; }

        public Task<bool> EnsureDatabaseAsync() {
            throw new NotImplementedException();
        }

        public Task MigrateDatabaseAsync() {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync() {
            throw new NotImplementedException();
        }
    }
}
