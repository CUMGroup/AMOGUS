using AMOGUS.Core.Common.Communication;
using AMOGUS.Core.Common.Exceptions;
using AMOGUS.Core.Common.Interfaces.Abstractions;
using AMOGUS.Core.Common.Interfaces.Database;
using AMOGUS.Core.Common.Interfaces.Game;
using AMOGUS.Core.Common.Interfaces.Repositories;
using AMOGUS.Core.Domain.Enums;
using AMOGUS.Core.Domain.Models.Entities;
using FluentValidation;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AMOGUS.UnitTests")]
namespace AMOGUS.Core.Services.Gameplay {
    internal class GameService : IGameService {

        private readonly IExerciseService _exerciseService;
        private readonly IStatsService _statsService;

        private readonly IGameSessionRepository _gameSessionRepository;

        private readonly IUserManager _userManager;

        private readonly IDateTime _dateTime;

        private readonly IValidator<GameSession> _gameSessionValidator;

        public GameService(IExerciseService exerciseService, IUserManager userManager, IStatsService statsService, IGameSessionRepository gameSessionRepository, IDateTime dateTime, IValidator<GameSession> gameSessionValidator) {
            _exerciseService = exerciseService!;
            _userManager = userManager!;
            _statsService = statsService!;
            _gameSessionRepository = gameSessionRepository!;
            _dateTime = dateTime!;
            _gameSessionValidator = gameSessionValidator!;
        }

        public async Task<Result> EndSessionAsync(GameSession session, string userId) {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) {
                return new RecordNotFoundException($"Could not find user with id {userId}");
            }
            session.SessionId = Guid.NewGuid().ToString();
            session.User = user;
            session.PlayedAt = _dateTime.Now;

            var validRes = _gameSessionValidator.Validate(session);
            if (!validRes.IsValid)
                return new ValidationException(validRes.Errors);

            var answers = CalculateCorrectAnswers(session.Questions);

            await _statsService.UpdateUserStatsAsync(session, answers, user);

            var dbRes = await _gameSessionRepository.AddGameSessionAsync(session);
            return dbRes > 0;
        }
        public GameSession NewSession(CategoryType category, string userId, int questionAmount) {
            var session = new GameSession() {
                Questions = _exerciseService.GetRandomExercises(category, questionAmount),
                SessionId = Guid.NewGuid().ToString(),
                UserId = userId,
                Category = category,
            };
            return session;
        }

        public GameSession NewSession(CategoryType category, string userId) {
            return NewSession(category, userId, 10);
        }

        private bool[] CalculateCorrectAnswers(List<Question> questions) {
            bool[] answers = new bool[questions.Count];
            for (int i = 0; i < questions.Count; ++i) {
                answers[i] = _exerciseService.CheckAnswer(questions[i]);
            }
            return answers;
        }
    }
}
