using AMOGUS.Core.Common.Communication;
using AMOGUS.Core.Common.Exceptions;
using AMOGUS.Core.Common.Interfaces.Abstractions;
using AMOGUS.Core.Common.Interfaces.Repositories;
using AMOGUS.Core.DataTransferObjects.User;
using AMOGUS.Core.Domain.Models.Entities;
using AMOGUS.Core.Services.Gameplay;
using FluentValidation;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
                    CorrectAnswers = 42+15
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
            Assert.True(result.Value.CorrectAnswers == 42+15, $"Correct Answers was actually {result.Value.CorrectAnswers} and not {42+15}");
        }
        #endregion

        #region UpdateUserStatsAsync(UserStats userStats)
        // return false
        // res = 0 (fehler)
        // res > 0
        #endregion

        #region UpdateUserStatsAsync(GameSession session, bool[] answers, ApplicationUser user)
        // return false
        // res = 0
        // res > 0
        // stats are updated
        // if not played so far, does streak increase?
        #endregion
    }
}
