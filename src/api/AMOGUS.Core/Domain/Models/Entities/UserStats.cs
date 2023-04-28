using AMOGUS.Infrastructure.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMOGUS.Core.Domain.Models.Entities {
    public class UserStats {

        // TODO: Annotation

        [Key]
        [ForeignKey("User")]
        public string UserId { get; set; } = string.Empty;

        public ApplicationUser? User { get; set; }

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
    }
}
