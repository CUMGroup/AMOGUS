﻿using AMOGUS.Core.Common.Communication;
using AMOGUS.Core.Common.Exceptions;
using AMOGUS.Core.Common.Interfaces.Database;
using AMOGUS.Core.Common.Interfaces.Game;
using AMOGUS.Core.Domain.Models.Entities;
using AMOGUS.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace AMOGUS.Core.Services.Gameplay {
    internal class StatsService : IStatsService {

        private readonly IApplicationDbContext _dbContext;

        public StatsService(IApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<Result<UserStats>> GetUserStatsAsync(string userId) {
            var res = await _dbContext.UserStats.Where(e => e.UserId.Equals(userId)).Include(e => e.User).FirstOrDefaultAsync();
            if (res == null) {
                return new UserOperationException($"Could not find stats for user with id {userId}");
            }
            return res;
        }

        public async Task<bool> UpdateUserStatsAsync(UserStats userStats) {
            _dbContext.UserStats.Update(userStats);
            var res = await _dbContext.SaveChangesAsync();
            return res > 0;
        }

        public async Task<bool> UpdateUserStatsAsync(GameSession session, bool[] answers, ApplicationUser user) {
            var userStats = (await GetUserStatsAsync(user.Id))
                .ValueOrDefault(
                    GetInitialUserStatsModel(session, user)
                );

            userStats.OverallAnswered += session.GivenAnswersCount;
            userStats.TotalTimePlayed += session.Playtime.TotalMilliseconds;
            userStats.User.PlayedToday = true;
            for (int i = 0; i < session.Questions.Count; ++i) {
                if (answers[i]) {
                    userStats.Level += session.Questions[i].ExperiencePoints;
                    ++userStats.CorrectAnswers;
                }
            }
            if (userStats.SlowestAnswer < session.SlowestAnswer.TotalMilliseconds)
                userStats.SlowestAnswer = session.SlowestAnswer.TotalMilliseconds;
            if (userStats.QuickestAnswer > session.QuickestAnswer.TotalMilliseconds)
                userStats.QuickestAnswer = session.QuickestAnswer.TotalMilliseconds;

            return await UpdateUserStatsAsync(userStats);
        }

        private UserStats GetInitialUserStatsModel(GameSession session, ApplicationUser user) {
            return new UserStats() {
                UserId = user.Id,
                User = user,
                QuickestAnswer = session.QuickestAnswer.TotalMilliseconds,
                SlowestAnswer = session.SlowestAnswer.TotalMilliseconds,
            };
        }
    }
}
