using AMOGUS.Core.Common.Interfaces.Database;
using AMOGUS.Core.Common.Interfaces.Game;
using AMOGUS.Core.Domain.Enums;
using AMOGUS.Core.Domain.Models.Entities;
using AMOGUS.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMOGUS.Core.Services.Gameplay {
    internal class GameService : IGameService {

        private readonly IExerciseService _exerciseService;
        private readonly IApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public GameService(IExerciseService exerciseService, IApplicationDbContext dbContext, Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager) {
            _exerciseService = exerciseService;
            _dbContext = dbContext;
            this.userManager = userManager;
        }

        public async Task EndSessionAsync(GameSession session, string userId) {
            var answers = new List<bool>();
            var user = await userManager.FindByIdAsync(userId);
            if(user == null) {
                return;
            }
            session.User = user;

            var userstat = await _dbContext.UserStats.Where(e => e.UserId.Equals(userId)).Include(e => e.User).FirstOrDefaultAsync();
            if(userstat == null) {
                userstat = new() {
                    UserId = userId,
                    User = user,
                    QuickestAnswer = session.QuickestAnswer.TotalMilliseconds,
                    SlowestAnswer = session.SlowestAnswer.TotalMilliseconds,
                };
                await _dbContext.UserStats.AddAsync(userstat);
                await _dbContext.SaveChangesAsync();
            }
            userstat.OverallAnswered += session.GivenAnswersCount;
            userstat.TotalTimePlayed += session.Playtime.TotalMilliseconds;
            userstat.User.PlayedToday = true;
            foreach(var quest in session.Questions) {
                if(await _exerciseService.CheckAnswerAsync(quest)) {
                    userstat.Level += quest.ExperiencePoints;
                    ++userstat.CorrectAnswers;
                }
            }
            if(userstat.SlowestAnswer < session.SlowestAnswer.TotalMilliseconds)
                userstat.SlowestAnswer = session.SlowestAnswer.TotalMilliseconds;
            if (userstat.QuickestAnswer > session.QuickestAnswer.TotalMilliseconds)
                userstat.QuickestAnswer = session.QuickestAnswer.TotalMilliseconds;
            //await userManager.UpdateAsync(userstat.User);
            _dbContext.GameSessions.Add(session);
            await _dbContext.SaveChangesAsync();
            _dbContext.UserStats.Update(userstat);
            await _dbContext.SaveChangesAsync();
        }

        public GameSession NewSession(CategoryType category, string userId) {
            var session = new GameSession() {
                Questions = _exerciseService.GetRandomExercises(category, 20),
                SessionId = Guid.NewGuid().ToString(),
                UserId = userId,
            };
            return session;
        }
    }
}
