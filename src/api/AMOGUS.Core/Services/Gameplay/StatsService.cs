using AMOGUS.Core.Common.Communication;
using AMOGUS.Core.Common.Exceptions;
using AMOGUS.Core.Common.Interfaces.Abstractions;
using AMOGUS.Core.Common.Interfaces.Game;
using AMOGUS.Core.Common.Interfaces.Repositories;
using AMOGUS.Core.DataTransferObjects.User;
using AMOGUS.Core.Domain.Enums;
using AMOGUS.Core.Domain.Models.Entities;
using AMOGUS.Infrastructure.Identity;
using System.ComponentModel.DataAnnotations;

namespace AMOGUS.Core.Services.Gameplay {
    internal class StatsService : IStatsService {

        private readonly IUserStatsRepository _userStatsRepository;
        private readonly IGameSessionRepository _gameSessionRepository;
        private readonly IDateTime _dateTime;

        public StatsService(IUserStatsRepository userStatsRepository, IGameSessionRepository gameSessionRepository, IDateTime dateTime) {
            _userStatsRepository = userStatsRepository!;
            _gameSessionRepository = gameSessionRepository!;
            _dateTime = dateTime!;
        }

        public async Task<Result<UserStats>> GetUserStatsAsync(string userId) {
            var res = await _userStatsRepository.GetUserStatsIncludeUserAsync(userId);
            if (res == null) {
                return new UserOperationException($"Could not find stats for user with id {userId}");
            }
            return res;
        }

        public async Task<Result<UserStatsApiModel>> GetDetailedUserStatsModel(string userId) {
            var res = await _userStatsRepository.GetUserStatsAsync(userId);
            if (res == null)
                return new RecordNotFoundException($"Could not find stats for user with id {userId}");
            var detailedUserStatsModel = UserStatsApiModel.MapFromUserStats(res);
            var gamesPlayed = await _gameSessionRepository.GetAllBy(e => e.UserId.Equals(userId));

            detailedUserStatsModel.CorrectAnswersPerDay = GetCorrectAnswersPerDay(gamesPlayed, 5);
            detailedUserStatsModel.CategorieAnswers = GetAnswersPerCategory(gamesPlayed);

            return detailedUserStatsModel;
        }

        private Dictionary<CategoryType, int> GetAnswersPerCategory(List<GameSession> gamesPlayed) {
            var categories = Enum.GetValues<CategoryType>();

            var answersPerCategory = new Dictionary<CategoryType, int>();
            foreach(var categorie in categories) {
                answersPerCategory.Add(
                    categorie,
                    gamesPlayed.Where(e => e.Category.Equals(categorie))
                        .Select(e => e.GivenAnswersCount)
                        .Aggregate(0, (x, y) => x + y)
                );
            }
            return answersPerCategory;
        }

        private Dictionary<DateTime, int> GetCorrectAnswersPerDay(List<GameSession> gamesPlayed, int days) {
            var lastFiveDays = GetLastDays(days);

            var correctAnswersPerDay = new Dictionary<DateTime, int>();
            foreach (var date in lastFiveDays) {
                correctAnswersPerDay.Add(
                    date,
                    gamesPlayed.Where(e => e.PlayedAt.Date.Equals(date.Date))
                        .Select(e => e.CorrectAnswersCount)
                        .Aggregate(0, (x, y) => x + y)
                );
            }

            return correctAnswersPerDay;
        }


        public async Task<bool> UpdateUserStatsAsync(UserStats userStats) {
            var res = await _userStatsRepository.UpdateUserStatsAsync(userStats);
            return res > 0;
        }

        public async Task<bool> UpdateUserStatsAsync(GameSession session, bool[] answers, ApplicationUser user) {
            var userStats = await (await GetUserStatsAsync(user.Id))
                .ValueOrDefaultAsync(
                    () => CreateInitialUserStatsModel(session, user)
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

        private async Task<UserStats> CreateInitialUserStatsModel(GameSession session, ApplicationUser user) {
            var stats = new UserStats() {
                UserId = user.Id,
                User = user,
                QuickestAnswer = session.QuickestAnswer.TotalMilliseconds,
                SlowestAnswer = session.SlowestAnswer.TotalMilliseconds,
            };
            await _userStatsRepository.AddUserStatsAsync(stats);
            return stats;
        }

        private DateTime[] GetLastDays(int amount) {
            var ret = new DateTime[amount];
            ret[0] = _dateTime.Now.Date;
            for(int i = 1; i < amount; ++i) {
                ret[i] = ret[i - 1].AddDays(-1);
            }

            return ret;
        }
    }
}
