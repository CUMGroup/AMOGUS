using AMOGUS.Core.Common.Communication;
using AMOGUS.Core.Common.Exceptions;
using AMOGUS.Core.Common.Interfaces.Database;
using AMOGUS.Core.Common.Interfaces.Game;
using AMOGUS.Core.Domain.Enums;
using AMOGUS.Core.Domain.Models.Entities;

namespace AMOGUS.Core.Services.Gameplay {
    internal class GameService : IGameService {

        private readonly IExerciseService _exerciseService;
        private readonly IApplicationDbContext _dbContext;
        private readonly IUserManager _userManager;
        private readonly IStatsService _statsService;

        public GameService(IExerciseService exerciseService, IApplicationDbContext dbContext, IUserManager userManager, IStatsService statsService) {
            _exerciseService = exerciseService;
            _dbContext = dbContext;
            _userManager = userManager;
            _statsService = statsService;
        }

        public async Task<Result> EndSessionAsync(GameSession session, string userId) {
            var user = await _userManager.FindByIdAsync(userId);
            if(user == null) {
                return new UserNotFoundException($"Could not find user with id {userId}"); // 15uhr - 18 uhr
            }
            session.User = user;

            var answers = await CalculateCorrectAnswersAsync(session.Questions);

            await _statsService.UpdateUserStatsAsync(session, answers, user);
            
            _dbContext.GameSessions.Add(session);
            await _dbContext.SaveChangesAsync();
        }
        public GameSession NewSession(CategoryType category, string userId, int questionAmount) {
            var session = new GameSession() {
                Questions = _exerciseService.GetRandomExercises(category, questionAmount),
                SessionId = Guid.NewGuid().ToString(),
                UserId = userId,
            };
            return session;
        }

        public GameSession NewSession(CategoryType category, string userId) {
            return NewSession(category, userId, 10);
        }

        private async Task<bool[]> CalculateCorrectAnswersAsync(List<Question> questions) {
            bool[] answers = new bool[questions.Count];
            for (int i = 0; i < questions.Count; ++i) {
                answers[i] = await _exerciseService.CheckAnswerAsync(questions[i]);
            }
            return answers;
        }
    }
}
