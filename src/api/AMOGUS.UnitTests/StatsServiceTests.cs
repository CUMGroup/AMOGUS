using AMOGUS.Core.Common.Exceptions;
using AMOGUS.Core.Common.Interfaces.Abstractions;
using AMOGUS.Core.Common.Interfaces.Repositories;
using AMOGUS.Core.DataTransferObjects.User;
using AMOGUS.Core.Domain.Models.Entities;
using AMOGUS.Core.Services.Gameplay;
using AMOGUS.Infrastructure.Identity;
using FluentValidation;
using FluentValidation.Results;
using System.Linq.Expressions;

namespace AMOGUS.UnitTests {
    public class StatsServiceTests {
        private Mock<IUserStatsRepository> CreateUserStatsRepositoryMock() => new();
        private Mock<IGameSessionRepository> CreateGameSessionRepositoryMock() => new();
        private Mock<IDateTime> CreateDateTimeMock() => new();
        private Mock<IValidator<UserStats>> CreateValidatorMock() => new();

        #region GetUserStatsAsync
        [Fact]
        public async Task GetUserStatsAsync_WhenGivenAUserId_AndUserHasNoStats_Exception() {
            var userStatsRepositoryMock = CreateUserStatsRepositoryMock();
            userStatsRepositoryMock
                .Setup(x => x.GetUserStatsIncludeUserAsync(It.IsAny<string>()))
                .ReturnsAsync((UserStats) null);

            var statsService = new StatsService(userStatsRepositoryMock.Object, CreateGameSessionRepositoryMock().Object, CreateDateTimeMock().Object, CreateValidatorMock().Object) { };

            var result = await statsService.GetUserStatsAsync("testID");

            Assert.True(result.IsFaulted);
            Assert.True(result.exception is UserOperationException);
        }

        [Fact]
        public async Task GetUserStatsAsync_WhenGivenAUserId_AndUserHasStats_ReturnsStats() {
            var userStatsRepositoryMock = CreateUserStatsRepositoryMock();
            userStatsRepositoryMock
                .Setup(x => x.GetUserStatsIncludeUserAsync(It.IsAny<string>()))
                .ReturnsAsync(new UserStats());

            var statsService = new StatsService(userStatsRepositoryMock.Object, CreateGameSessionRepositoryMock().Object, CreateDateTimeMock().Object, CreateValidatorMock().Object) { };

            var result = await statsService.GetUserStatsAsync("testID");

            Assert.True(result.IsSuccess);
            Assert.True(result.Value is UserStats);
        }
        #endregion

        #region GetDetailedUserStatsModelAsync
        [Fact]
        public async Task GetDetailedUserStatsModelAsync_WhenGivenAUserId_AndUserHasNoStats_Exception() {
            var userStatsRepositoryMock = CreateUserStatsRepositoryMock();
            userStatsRepositoryMock
                .Setup(x => x.GetUserStatsAsync(It.IsAny<string>()))
                .ReturnsAsync((UserStats) null);

            var statsService = new StatsService(userStatsRepositoryMock.Object, CreateGameSessionRepositoryMock().Object, CreateDateTimeMock().Object, CreateValidatorMock().Object) { };

            var result = await statsService.GetDetailedUserStatsModelAsync("testID");

            Assert.True(result.IsFaulted);
            Assert.True(result.exception is RecordNotFoundException);
        }

        [Fact]
        public async Task GetDetailedUserStatsModelAsync_WhenGivenAUserId_EverythingAlright_ReturnsDetailedUserstats() {
            var testGamesList = new List<GameSession>() {
                new GameSession() {
                    CorrectAnswersCount = 42,
                    Category = Core.Domain.Enums.CategoryType.MENTAL,
                    PlayedAt = new DateTime(2023, 5, 3)
                },
                new GameSession() {
                    CorrectAnswersCount = 15,
                    Category = Core.Domain.Enums.CategoryType.MENTAL,
                    PlayedAt = new DateTime(2023, 5, 2)
                }
            };

            var userStatsRepositoryMock = CreateUserStatsRepositoryMock();
            userStatsRepositoryMock
                .Setup(x => x.GetUserStatsAsync(It.IsAny<string>()))
                .ReturnsAsync(new UserStats() {
                    CorrectAnswers = 42 + 15
                });

            var gameSessionRepositoryMock = CreateGameSessionRepositoryMock();
            gameSessionRepositoryMock
                .Setup(x => x.GetAllBy(It.IsAny<Expression<Func<GameSession, bool>>>()))
                .Returns(Task.FromResult(testGamesList));

            var dateTimeMock = CreateDateTimeMock();
            dateTimeMock
                .Setup(x => x.Now)
                .Returns(new DateTime(2023, 5, 3));

            var statsService = new StatsService(userStatsRepositoryMock.Object, gameSessionRepositoryMock.Object, dateTimeMock.Object, CreateValidatorMock().Object) { };

            var result = await statsService.GetDetailedUserStatsModelAsync("testID");
            int value = 42 + 15;

            Assert.True(result.IsSuccess, "Result was faulted");
            Assert.True(result.Value is UserStatsApiModel, "Result is not UserStatsApiModel");
            Assert.True(result.Value.CategorieAnswers.TryGetValue(Core.Domain.Enums.CategoryType.MENTAL, out value));
            Assert.True(result.Value.CorrectAnswers == 42 + 15, $"Correct Answers was actually {result.Value.CorrectAnswers} and not {42 + 15}");
        }
        #endregion

        #region UpdateUserStatsAsync(UserStats userStats)
        [Fact]
        public async Task UpdateUserStatsAsync_WhenGivenUserStats_AndValidationFails_ReturnsFalse() {
            var statsValidatorMock = CreateValidatorMock();
            statsValidatorMock
                .Setup(x => x.Validate(It.IsAny<UserStats>()))
                .Returns(new ValidationResult(new List<ValidationFailure>() {
                    new ValidationFailure("Test", "Test error lol")
                }));

            var statsService = new StatsService(CreateUserStatsRepositoryMock().Object, CreateGameSessionRepositoryMock().Object, CreateDateTimeMock().Object, statsValidatorMock.Object) { };

            var result = await statsService.UpdateUserStatsAsync(new UserStats());

            Assert.False(result);
        }

        [Fact]
        public async Task UpdateUserStatsAsync_WhenGivenUserStats_UpdateInDbFails_ReturnsFalse() {
            var statsValidatorMock = CreateValidatorMock();
            statsValidatorMock
                .Setup(x => x.Validate(It.IsAny<UserStats>()))
                .Returns(new ValidationResult());

            var userStatsRepositoryMock = CreateUserStatsRepositoryMock();
            userStatsRepositoryMock
                .Setup(x => x.UpdateUserStatsAsync(It.IsAny<UserStats>()))
                .ReturnsAsync(0);

            var statsService = new StatsService(userStatsRepositoryMock.Object, CreateGameSessionRepositoryMock().Object, CreateDateTimeMock().Object, statsValidatorMock.Object) { };

            var result = await statsService.UpdateUserStatsAsync(new UserStats());

            Assert.False(result);
        }

        [Fact]
        public async Task UpdateUserStatsAsync_WhenGivenUserStats_AndEverythingIsFine_ReturnsTrue() {
            var statsValidatorMock = CreateValidatorMock();
            statsValidatorMock
                .Setup(x => x.Validate(It.IsAny<UserStats>()))
                .Returns(new ValidationResult());

            var userStatsRepositoryMock = CreateUserStatsRepositoryMock();
            userStatsRepositoryMock
                .Setup(x => x.UpdateUserStatsAsync(It.IsAny<UserStats>()))
                .ReturnsAsync(1);

            var statsService = new StatsService(userStatsRepositoryMock.Object, CreateGameSessionRepositoryMock().Object, CreateDateTimeMock().Object, statsValidatorMock.Object) { };

            var result = await statsService.UpdateUserStatsAsync(new UserStats());

            Assert.True(result);
        }
        #endregion

        #region UpdateUserStatsAsync(GameSession session, bool[] answers, ApplicationUser user)
        // return false
        [Fact]
        public async Task UpdateUserStatsAsync_WhenGivenGameSessionAnswersAndUser_AndValidationFails_ReturnsFalse() {
            var statsValidatorMock = CreateValidatorMock();
            statsValidatorMock
                .Setup(x => x.Validate(It.IsAny<UserStats>()))
                .Returns(new ValidationResult(new List<ValidationFailure>() {
                    new ValidationFailure("Test", "Test error lol")
                }));

            var userStatsRepositoryMock = CreateUserStatsRepositoryMock();
            userStatsRepositoryMock
                .Setup(x => x.GetUserStatsIncludeUserAsync(It.IsAny<string>()))
                .ReturnsAsync(new UserStats());

            var statsService = new StatsService(userStatsRepositoryMock.Object, CreateGameSessionRepositoryMock().Object, CreateDateTimeMock().Object, statsValidatorMock.Object) { };

            bool[] answers = { true, false };

            var result = await statsService.UpdateUserStatsAsync(new GameSession(), answers, new ApplicationUser());

            Assert.False(result);
        }

        // res = 0
        [Fact]
        public async Task UpdateUserStatsAsync_WhenGivenGameSessionAnswersAndUser_UpdateInDbFails_ReturnsFalse() {
            var statsValidatorMock = CreateValidatorMock();
            statsValidatorMock
                .Setup(x => x.Validate(It.IsAny<UserStats>()))
                .Returns(new ValidationResult());

            var userStatsRepositoryMock = CreateUserStatsRepositoryMock();
            userStatsRepositoryMock
                .Setup(x => x.UpdateUserStatsAsync(It.IsAny<UserStats>()))
                .ReturnsAsync(0);
            userStatsRepositoryMock
                .Setup(x => x.GetUserStatsIncludeUserAsync(It.IsAny<string>()))
                .ReturnsAsync(new UserStats());

            var statsService = new StatsService(userStatsRepositoryMock.Object, CreateGameSessionRepositoryMock().Object, CreateDateTimeMock().Object, statsValidatorMock.Object) { };

            bool[] answers = { true, false };

            var result = await statsService.UpdateUserStatsAsync(new GameSession(), answers, new ApplicationUser());

            Assert.False(result);
        }

        // res > 0
        [Fact]
        public async Task UpdateUserStatsAsync_WhenGivenGameSessionAnswersAndUser_AndEverythingIsFine_ReturnsTrue() {
            var statsValidatorMock = CreateValidatorMock();
            statsValidatorMock
                .Setup(x => x.Validate(It.IsAny<UserStats>()))
                .Returns(new ValidationResult());

            var userStatsRepositoryMock = CreateUserStatsRepositoryMock();
            userStatsRepositoryMock
                .Setup(x => x.UpdateUserStatsAsync(It.IsAny<UserStats>()))
                .ReturnsAsync(1);
            userStatsRepositoryMock
                .Setup(x => x.GetUserStatsIncludeUserAsync(It.IsAny<string>()))
                .ReturnsAsync(new UserStats());

            var statsService = new StatsService(userStatsRepositoryMock.Object, CreateGameSessionRepositoryMock().Object, CreateDateTimeMock().Object, statsValidatorMock.Object) { };

            bool[] answers = { true, false };

            var result = await statsService.UpdateUserStatsAsync(new GameSession(), answers, new ApplicationUser());

            Assert.True(result);
        }

        // stats are updated
        [Fact]
        public async Task UpdateUserStatsAsync_WhenGivenGameSessionAnswersAndUser_AndEverythingIsFine_StatsAreUpdatedCorrectly() {
            var testStats = new UserStats() {
                UserId = "testId",
                Level = 1,
                CurrentStreak = 1,
                OverallAnswered = 2,
                CorrectAnswers = 0,
                TotalTimePlayed = 5,
                QuickestAnswer = 5,
                SlowestAnswer = 10,
                LongestStreak = 1
            };

            var statsValidatorMock = CreateValidatorMock();
            statsValidatorMock
                .Setup(x => x.Validate(It.IsAny<UserStats>()))
                .Returns(new ValidationResult());

            var userStatsRepositoryMock = CreateUserStatsRepositoryMock();
            userStatsRepositoryMock
                .Setup(x => x.UpdateUserStatsAsync(It.IsAny<UserStats>()))
                .ReturnsAsync(1);
            userStatsRepositoryMock
                .Setup(x => x.GetUserStatsIncludeUserAsync(It.IsAny<string>()))
                .ReturnsAsync(testStats);

            var statsService = new StatsService(userStatsRepositoryMock.Object, CreateGameSessionRepositoryMock().Object, CreateDateTimeMock().Object, statsValidatorMock.Object) { };

            var testGameSession = new GameSession() {
                SessionId = "testSession",
                UserId = "testId",
                Playtime = 5,
                CorrectAnswersCount = 0,
                QuickestAnswer = 12,
                SlowestAnswer = 10,
            };
            bool[] answers = { false, false };

            var result = await statsService.UpdateUserStatsAsync(testGameSession, answers, new ApplicationUser());

            Assert.True(result);
            Assert.True(testStats.TotalTimePlayed == 10);
            Assert.True(testStats.CorrectAnswers == 0);
            Assert.True(testStats.QuickestAnswer == 5);
            Assert.True(testStats.SlowestAnswer == 10);
        }

        [Fact]
        public async Task UpdateUserStatsAsync_WhenGivenGameSessionAnswersAndUser_AndEverythingIsFine_StatsAreUpdatedCorrectly2() {
            var testStats = new UserStats() {
                UserId = "testId",
                User = new ApplicationUser(),
                Level = 1,
                CurrentStreak = 1,
                OverallAnswered = 2,
                CorrectAnswers = 0,
                TotalTimePlayed = 5,
                QuickestAnswer = 5,
                SlowestAnswer = 10,
                LongestStreak = 1
            };

            var statsValidatorMock = CreateValidatorMock();
            statsValidatorMock
                .Setup(x => x.Validate(It.IsAny<UserStats>()))
                .Returns(new ValidationResult());

            var userStatsRepositoryMock = CreateUserStatsRepositoryMock();
            userStatsRepositoryMock
                .Setup(x => x.UpdateUserStatsAsync(It.IsAny<UserStats>()))
                .ReturnsAsync(1);
            userStatsRepositoryMock
                .Setup(x => x.GetUserStatsIncludeUserAsync(It.IsAny<string>()))
                .ReturnsAsync(testStats);

            var statsService = new StatsService(userStatsRepositoryMock.Object, CreateGameSessionRepositoryMock().Object, CreateDateTimeMock().Object, statsValidatorMock.Object) { };

            var testGameSession = new GameSession() {
                SessionId = "testSession",
                UserId = "testId",
                Playtime = 5,
                CorrectAnswersCount = 1,
                QuickestAnswer = 2,
                SlowestAnswer = 11,
                Questions = new List<Question>() { new Question(), new Question() }
            };
            bool[] answers = { true, false };

            var result = await statsService.UpdateUserStatsAsync(testGameSession, answers, new ApplicationUser());

            Assert.True(result);
            Assert.True(testStats.TotalTimePlayed == 10);
            Assert.True(testStats.CorrectAnswers == 1);
            Assert.True(testStats.QuickestAnswer == 2);
            Assert.True(testStats.SlowestAnswer == 11);
        }

        // [TODO] if not played so far, does streak increase? [TODO]
        #endregion
    }
}
