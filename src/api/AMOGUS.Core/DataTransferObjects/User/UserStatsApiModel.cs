using AMOGUS.Core.Domain.Enums;
using AMOGUS.Core.Domain.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMOGUS.Core.DataTransferObjects.User {
    public class UserStatsApiModel {

        [Key]
        [ForeignKey("User")]
        public string UserId { get; set; }

        [Range(0, int.MaxValue)]
        public int Level { get; set; } = 0;

        [Range(0, int.MaxValue)]
        public int CurrentStreak { get; set; } = 0;

        [Range(0, int.MaxValue)]
        public int OverallAnswered { get; set; } = 0;

        [Range(0, int.MaxValue)]
        public int CorrectAnswers { get; set; } = 0;

        [Range(0, double.MaxValue)]
        public double TotalTimePlayed { get; set; } = 0;

        [Range(0, double.MaxValue)]
        public double? QuickestAnswer { get; set; }

        [Range(0, double.MaxValue)]
        public double? SlowestAnswer { get; set; }

        [Range(0, int.MaxValue)]
        public int LongestStreak { get; set; } = 0;

        public Dictionary<CategoryType, int> CategorieAnswers { get; set; } = new();

        public Dictionary<DateTime, int> CorrectAnswersPerDay { get; set; } = new();


        public static UserStatsApiModel MapFromUserStats(UserStats stats) {
            return new UserStatsApiModel {
                UserId = stats.UserId,
                Level = stats.Level,
                CurrentStreak = stats.CurrentStreak,
                CorrectAnswers = stats.CorrectAnswers,
                LongestStreak = stats.LongestStreak,
                OverallAnswered = stats.OverallAnswered,
                QuickestAnswer = stats.QuickestAnswer,
                SlowestAnswer = stats.SlowestAnswer,
                TotalTimePlayed = stats.TotalTimePlayed
            };
        }
    }
}
