using AMOGUS.Core.Common.Exceptions;
using AMOGUS.Core.Common.Interfaces.Abstractions;
using AMOGUS.Core.Common.Interfaces.Database;
using AMOGUS.Core.Common.Interfaces.Game;
using AMOGUS.Core.Common.Interfaces.Repositories;
using AMOGUS.Core.Domain.Enums;
using AMOGUS.Core.Domain.Models.Entities;
using AMOGUS.Core.Services.Gameplay;
using AMOGUS.Infrastructure.Identity;
using FluentValidation;
using FluentValidation.Results;

namespace AMOGUS.UnitTests.Tests.Core {
    public class GameServiceTests {
        private Mock<IExerciseService> CreateExerciseServiceMock() => new();
        private Mock<IStatsService> CreateStatsServiceMock() => new();
        private Mock<IGameSessionRepository> CreateGameSessionRepositoryMock() => new();
        private Mock<IUserManager> CreateUserManagerMock() => new();
        private Mock<IDateTime> CreateDateTimeMock() => new();
        private Mock<IValidator<GameSession>> CreateValidatorMock() => new();

        #region NewSession 
        [Fact]
        public void NewSession_WithoutQuestionAmount_ReturnsSessionWith10Questions() {
            var exerciseServiceMock = CreateExerciseServiceMock();
            exerciseServiceMock
                .Setup(x => x.GetRandomExercises(It.IsAny<CategoryType>(), 10))
                .Returns(new List<Question>() { new Question(), new Question(), new Question(), new Question(), new Question(), new Question(), new Question(), new Question(), new Question(), new Question() });

            var gameService = new GameService(exerciseServiceMock.Object, CreateUserManagerMock().Object, CreateStatsServiceMock().Object, CreateGameSessionRepositoryMock().Object, CreateDateTimeMock().Object, CreateValidatorMock().Object);

            var result = gameService.NewSession(CategoryType.ANALYSIS, "test");

            Assert.True(result.Questions.Count == 10);
        }

        [Fact]
        public void NewSession_WithQuestionAmount1_ReturnsSessionWith1Question() {
            var exerciseServiceMock = CreateExerciseServiceMock();
            exerciseServiceMock
                .Setup(x => x.GetRandomExercises(It.IsAny<CategoryType>(), It.IsAny<int>()))
                .Returns(new List<Question>() { new Question() });

            var gameService = new GameService(exerciseServiceMock.Object, CreateUserManagerMock().Object, CreateStatsServiceMock().Object, CreateGameSessionRepositoryMock().Object, CreateDateTimeMock().Object, CreateValidatorMock().Object);

            var result = gameService.NewSession(CategoryType.ANALYSIS, "test", 1);

            Assert.True(result.Questions.Count == 1);
        }
        #endregion

        #region EndSessionAsync
        [Fact]
        public async Task EndSessionAsync_AndUserNotFound_ReturnsException() {
            var userServiceMock = CreateUserManagerMock();
            userServiceMock
                .Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((ApplicationUser) null);

            var gameService = new GameService(CreateExerciseServiceMock().Object, userServiceMock.Object, CreateStatsServiceMock().Object, CreateGameSessionRepositoryMock().Object, CreateDateTimeMock().Object, CreateValidatorMock().Object);

            var result = await gameService.EndSessionAsync(new GameSession(), "test");

            Assert.True(result.IsFaulted);
            Assert.True(result.exception is RecordNotFoundException);
        }

        [Fact]
        public async Task EndSessionAsync_AndUserFound_AndValidationFails_ReturnsException() {
            var userServiceMock = CreateUserManagerMock();
            userServiceMock
                .Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new ApplicationUser());

            var dateTimeMock = CreateDateTimeMock();
            dateTimeMock
                .Setup(x => x.Now)
                .Returns(DateTime.Now);

            var validatorMock = CreateValidatorMock();
            validatorMock
                .Setup(x => x.Validate(It.IsAny<GameSession>()))
                .Returns(new ValidationResult(new List<ValidationFailure>() {
                    new ValidationFailure("Test", "Test error lol")
                }));

            var gameService = new GameService(CreateExerciseServiceMock().Object, userServiceMock.Object, CreateStatsServiceMock().Object, CreateGameSessionRepositoryMock().Object, dateTimeMock.Object, validatorMock.Object);

            var result = await gameService.EndSessionAsync(new GameSession(), "test");

            Assert.True(result.IsFaulted);
            Assert.True(result.exception is ValidationException);
        }

        [Fact]
        public async Task EndSessionAsync_AndUserFound_AndSessionValid_ButDbNotAffected_ReturnsFaulted() {
            var userServiceMock = CreateUserManagerMock();
            userServiceMock
                .Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new ApplicationUser());

            var dateTimeMock = CreateDateTimeMock();
            dateTimeMock
                .Setup(x => x.Now)
                .Returns(DateTime.Now);

            var validatorMock = CreateValidatorMock();
            validatorMock
                .Setup(x => x.Validate(It.IsAny<GameSession>()))
                .Returns(new ValidationResult());

            var statsServiceMock = CreateStatsServiceMock();
            statsServiceMock
                .Setup(x => x.UpdateUserStatsAsync(It.IsAny<GameSession>(), It.IsAny<bool[]>(), It.IsAny<ApplicationUser>()))
                .ReturnsAsync(true);

            var gameSessionRepositoryMock = CreateGameSessionRepositoryMock();
            gameSessionRepositoryMock
                .Setup(x => x.AddGameSessionAsync(It.IsAny<GameSession>()))
                .ReturnsAsync(0);

            var gameService = new GameService(CreateExerciseServiceMock().Object, userServiceMock.Object, statsServiceMock.Object, gameSessionRepositoryMock.Object, dateTimeMock.Object, validatorMock.Object);

            var testSession = new GameSession() {
                Questions = new List<Question>()
            };
            var result = await gameService.EndSessionAsync(testSession, "test");

            Assert.True(result.IsFaulted);
        }

        [Fact]
        public async Task EndSessionAsync_AndUserFound_AndSessionValid_AndDbAffected_ReturnsSuccess() {
            var userServiceMock = CreateUserManagerMock();
            userServiceMock
                .Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new ApplicationUser());

            var dateTimeMock = CreateDateTimeMock();
            dateTimeMock
                .Setup(x => x.Now)
                .Returns(DateTime.Now);

            var validatorMock = CreateValidatorMock();
            validatorMock
                .Setup(x => x.Validate(It.IsAny<GameSession>()))
                .Returns(new ValidationResult());

            var statsServiceMock = CreateStatsServiceMock();
            statsServiceMock
                .Setup(x => x.UpdateUserStatsAsync(It.IsAny<GameSession>(), It.IsAny<bool[]>(), It.IsAny<ApplicationUser>()))
                .ReturnsAsync(true);

            var gameSessionRepositoryMock = CreateGameSessionRepositoryMock();
            gameSessionRepositoryMock
                .Setup(x => x.AddGameSessionAsync(It.IsAny<GameSession>()))
                .ReturnsAsync(1);

            var gameService = new GameService(CreateExerciseServiceMock().Object, userServiceMock.Object, statsServiceMock.Object, gameSessionRepositoryMock.Object, dateTimeMock.Object, validatorMock.Object);

            var testSession = new GameSession() {
                Questions = new List<Question>()
            };
            var result = await gameService.EndSessionAsync(testSession, "test");

            Assert.True(result.IsSuccess);
        }
        #endregion
    }
}
