using AMOGUS.Core.Common.Exceptions;
using AMOGUS.Core.Common.Interfaces.Database;
using AMOGUS.Core.Common.Interfaces.Game;
using AMOGUS.Core.Domain.Models.Entities;
using AMOGUS.Core.Services.Gameplay;
using AMOGUS.Infrastructure.Identity;

namespace AMOGUS.UnitTests.Tests.Core {
    public class StreakServiceTests {
        private Mock<IUserManager> CreateUserManagerMock() => new();
        private Mock<IStatsService> CreateStatsServiceMock() => new();

        #region ReadStreakAsync
        [Fact]
        public async Task ReadStreakAsync_WhenGivenAUserId_AndUserHasNoStats_Exception() {
            var statsServiceMock = CreateStatsServiceMock();
            statsServiceMock
                .Setup(x => x.GetUserStatsAsync(It.IsAny<string>()))
                .ReturnsAsync(new UserOperationException());

            var streaksService = new StreaksService(statsServiceMock.Object, CreateUserManagerMock().Object) { };

            var result = await streaksService.ReadStreakAsync("testID");

            Assert.True(result.IsFaulted);
            Assert.True(result.exception is UserOperationException);
        }

        [Fact]
        public async Task ReadStreakAsync_WhenGivenAUserId_AndUserHasStats_ReturnsCurrentStreak() {
            var statsServiceMock = CreateStatsServiceMock();
            statsServiceMock
                .Setup(x => x.GetUserStatsAsync(It.IsAny<string>()))
                .ReturnsAsync(new UserStats() { CurrentStreak = 420 });

            var streaksService = new StreaksService(statsServiceMock.Object, CreateUserManagerMock().Object) { };

            var result = await streaksService.ReadStreakAsync("testID");

            Assert.True(result.IsSuccess);
            Assert.True(result == 420);
        }
        #endregion

        #region UpdateAllStreaksAsync
        [Fact]
        public async Task UpdateAllStreaksAsync_WhenPlayerHasNotPlayedToday_AndNotLongestStreak_StreakIsSetTo0() {
            var testUser = new ApplicationUser() {
                Id = "testID",
                PlayedToday = false
            };
            var testStats = new UserStats() {
                User = testUser,
                CurrentStreak = 420,
                LongestStreak = 600
            };

            var statsServiceMock = CreateStatsServiceMock();
            statsServiceMock
                .Setup(x => x.GetUserStatsAsync(It.IsAny<string>()))
                .ReturnsAsync(testStats);

            var userManagerMock = CreateUserManagerMock();
            userManagerMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<ApplicationUser>() { testUser });

            var streaksService = new StreaksService(statsServiceMock.Object, userManagerMock.Object) { };

            await streaksService.UpdateAllStreaksAsync();

            Assert.True(testStats.CurrentStreak == 0);
            Assert.True(testStats.LongestStreak == 600);
            Assert.False(testUser.PlayedToday);
        }

        [Fact]
        public async Task UpdateAllStreaksAsync_WhenPlayerHasNotPlayedToday_AndLongestStreak_StreakIsSetTo0_ButKeepLongest() {
            var testUser = new ApplicationUser() {
                Id = "testID",
                PlayedToday = false
            };
            var testStats = new UserStats() {
                User = testUser,
                CurrentStreak = 420,
                LongestStreak = 420
            };

            var statsServiceMock = CreateStatsServiceMock();
            statsServiceMock
                .Setup(x => x.GetUserStatsAsync(It.IsAny<string>()))
                .ReturnsAsync(testStats);

            var userManagerMock = CreateUserManagerMock();
            userManagerMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<ApplicationUser>() { testUser });

            var streaksService = new StreaksService(statsServiceMock.Object, userManagerMock.Object) { };

            await streaksService.UpdateAllStreaksAsync();

            Assert.True(testStats.CurrentStreak == 0, "current streak was not set to 0");
            Assert.True(testStats.LongestStreak == 420, "longest streak was lost");
            Assert.False(testUser.PlayedToday);
        }

        [Fact]
        public async Task UpdateAllStreaksAsync_WhenPlayerHasPlayedToday_ButNotLongestStreak_StreakIsIncreased() {
            var testUser = new ApplicationUser() {
                Id = "testID",
                PlayedToday = true
            };
            var testStats = new UserStats() {
                User = testUser,
                CurrentStreak = 420,
                LongestStreak = 600
            };

            var statsServiceMock = CreateStatsServiceMock();
            statsServiceMock
                .Setup(x => x.GetUserStatsAsync(It.IsAny<string>()))
                .ReturnsAsync(testStats);

            var userManagerMock = CreateUserManagerMock();
            userManagerMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<ApplicationUser>() { testUser });

            var streaksService = new StreaksService(statsServiceMock.Object, userManagerMock.Object) { };

            await streaksService.UpdateAllStreaksAsync();

            Assert.True(testStats.CurrentStreak == 421, "current streak was not increased");
            Assert.True(testStats.LongestStreak == 600, "current streak was modified");
            Assert.False(testUser.PlayedToday, "user has played");
        }

        [Fact]
        public async Task UpdateAllStreaksAsync_WhenPlayerHasPlayedToday_AndNewLongestStreak_StreakIsIncreased() {
            var testUser = new ApplicationUser() {
                Id = "testID",
                PlayedToday = true
            };
            var testStats = new UserStats() {
                User = testUser,
                CurrentStreak = 420,
                LongestStreak = 420
            };

            var statsServiceMock = CreateStatsServiceMock();
            statsServiceMock
                .Setup(x => x.GetUserStatsAsync(It.IsAny<string>()))
                .ReturnsAsync(testStats);

            var userManagerMock = CreateUserManagerMock();
            userManagerMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<ApplicationUser>() { testUser });

            var streaksService = new StreaksService(statsServiceMock.Object, userManagerMock.Object) { };

            await streaksService.UpdateAllStreaksAsync();

            Assert.True(testStats.CurrentStreak == 421, "current streak was not increased");
            Assert.True(testStats.LongestStreak == 421, "current streak was not increased");
            Assert.False(testUser.PlayedToday, "user has played");
        }

        [Fact]
        public async Task UpdateAllStreaksAsync_StreaksOfAllPlayersAreUpdated() {
            var testUsersList = new List<ApplicationUser>();
            var testStatsList = new List<UserStats>();
            var testStatsQueue = new Queue<UserStats>();


            for (int i = 0; i < 5; i++) {
                var testUser = new ApplicationUser() {
                    Id = $"{i}",
                    PlayedToday = true
                };
                var testStats = new UserStats() {
                    User = testUser,
                    CurrentStreak = 420,
                    LongestStreak = 420
                };

                testUsersList.Add(testUser);
                testStatsList.Add(testStats);
                testStatsQueue.Enqueue(testStats);
            }

            var userManagerMock = CreateUserManagerMock();
            userManagerMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(testUsersList);

            var statsServiceMock = CreateStatsServiceMock();
            statsServiceMock
                .Setup(x => x.GetUserStatsAsync(It.IsAny<string>()))
                .ReturnsAsync(() => testStatsQueue.Dequeue());

            var streaksService = new StreaksService(statsServiceMock.Object, userManagerMock.Object) { };

            await streaksService.UpdateAllStreaksAsync();

            foreach (var testStats in testStatsList) {
                Assert.True(testStats.CurrentStreak == 421, $"current streak was not increased User: {testStats?.User?.Id}");
                Assert.True(testStats?.LongestStreak == 421, "current streak was not increased");
            }
        }
        #endregion
    }
}
