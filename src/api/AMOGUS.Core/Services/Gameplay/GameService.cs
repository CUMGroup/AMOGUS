using AMOGUS.Core.Common.Interfaces.Database;
using AMOGUS.Core.Common.Interfaces.Game;
using AMOGUS.Core.Domain.Enums;
using AMOGUS.Core.Domain.Models.Entities;
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

        public GameService(IExerciseService exerciseService, IApplicationDbContext dbContext) {
            _exerciseService = exerciseService;
            _dbContext = dbContext;
        }

        public async Task EndSessionAsync(GameSession session, string userId) {
            var answers = new List<bool>();
            var userstat = await _dbContext.UserStats.Where(e => e.UserId.Equals(userId)).Include(e => e.User).FirstOrDefaultAsync();
            if(userstat == null) {
                return;
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
            _dbContext.GameSessions.Add(session);
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
