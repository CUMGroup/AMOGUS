
using AMOGUS.Core.Common.Interfaces.Repositories;
using AMOGUS.Core.Domain.Models.Entities;
using AMOGUS.Core.Services.Information;
using AMOGUS.Infrastructure.Identity;
using System.Linq.Expressions;

namespace AMOGUS.UnitTests.Tests.Core {
    public class LeaderboardServiceTests {

        private Mock<IUserStatsRepository> CreateStatsRepoMock() => new();

        [Fact]
        public async Task GetLeaderboardAsync_ReturnsCorrectObject() {

            var statsLongest = new List<UserStats> {
                new UserStats {
                    User = new ApplicationUser { UserName = "Testuser1Longest" },
                    CurrentStreak = 1,
                    LongestStreak = 10,
                    CorrectAnswers = 2,
                    OverallAnswered = 10
                },
                new UserStats {
                    User = new ApplicationUser { UserName = "Testuser2Longest" },
                    CurrentStreak = 2,
                    LongestStreak = 11,
                    CorrectAnswers = 2,
                    OverallAnswered = 10
                },
                new UserStats {
                    User = new ApplicationUser { UserName = "Testuser3Longest" },
                    CurrentStreak = 3,
                    LongestStreak = 12,
                    CorrectAnswers = 2,
                    OverallAnswered = 10
                },
            };
            var statsCurrent = new List<UserStats> {
                new UserStats {
                    User = new ApplicationUser { UserName = "Testuser1Current" },
                    CurrentStreak = 4,
                    LongestStreak = 13,
                    CorrectAnswers = 2,
                    OverallAnswered = 10
                },
                new UserStats {
                    User = new ApplicationUser { UserName = "Testuser2Current" },
                    CurrentStreak = 5,
                    LongestStreak = 14,
                    CorrectAnswers = 2,
                    OverallAnswered = 10
                },
            };
            var statsCR = new List<UserStats> {
                new UserStats {
                    User = new ApplicationUser { UserName = "Testuser1Cr" },
                    CurrentStreak = 6,
                    LongestStreak = 15,
                    CorrectAnswers = 2,
                    OverallAnswered = 10
                },
                new UserStats {
                    User = new ApplicationUser { UserName = "Testuser2Cr" },
                    CurrentStreak = 7,
                    LongestStreak = 16,
                    CorrectAnswers = 2,
                    OverallAnswered = 10
                },
                new UserStats {
                    User = new ApplicationUser { UserName = "Testuser3Cr" },
                    CurrentStreak = 8,
                    LongestStreak = 17,
                    CorrectAnswers = 2,
                    OverallAnswered = 10
                },
                new UserStats {
                    User = new ApplicationUser { UserName = "Testuser4Cr" },
                    CurrentStreak = 9,
                    LongestStreak = 18,
                    CorrectAnswers = 2,
                    OverallAnswered = 10
                },
            };


            var statsRepo = CreateStatsRepoMock();
            statsRepo
                .SetupSequence(x => x.GetTopOrderedByAsync(It.IsAny<int>(), It.IsAny<Expression<Func<UserStats, It.IsAnyType>>>()))
                .ReturnsAsync(statsLongest)
                .ReturnsAsync(statsCurrent)
                .ReturnsAsync(statsCR);


            var leaderboardService = new LeaderboardService(statsRepo.Object);

            var result = await leaderboardService.GetLeaderboardAsync();

            Assert.True(result.CurrentStreaks.Length == statsCurrent.Count);
            Assert.True(result.LongestStreaks.Length == statsLongest.Count);
            Assert.True(result.CorrectRatios.Length == statsCR.Count);

            for(int i = 0; i < statsLongest.Count; ++i) {
                Assert.True(result.LongestStreaks[i].Username.Equals(statsLongest[i].User!.UserName));
                Assert.True(result.LongestStreaks[i].Streak == statsLongest[i].LongestStreak);
            }
            for (int i = 0; i < statsCurrent.Count; ++i) {
                Assert.True(result.CurrentStreaks[i].Username.Equals(statsCurrent[i].User!.UserName));
                Assert.True(result.CurrentStreaks[i].Streak == statsCurrent[i].CurrentStreak);
            }
            for (int i = 0; i < statsCR.Count; ++i) {
                Assert.True(result.CorrectRatios[i].Username.Equals(statsCR[i].User!.UserName));
                Assert.True(result.CorrectRatios[i].CorrectRatio == statsCR[i].GetCorrectRatio());
            }
        }
    }
}
