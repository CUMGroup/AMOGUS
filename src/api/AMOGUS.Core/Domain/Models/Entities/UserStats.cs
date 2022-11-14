using AMOGUS.Infrastructure.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMOGUS.Core.Domain.Models.Entities {
    public class UserStats {

        // TODO: Annotation

        [Key]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int Level { get; set; } = 0;

        public int CurrentStreak { get; set; } = 0;

        public int OverallAnswered { get; set; } = 0;
        public int CorrectAnswers { get; set; } = 0;
        public double TotalTimePlayed { get; set; } = 0;
        public double QuickestAnswer { get; set; } = 0;
        public double SlowestAnswer { get; set; } = 0;
        public int LongestStreak { get; set; } = 0;
    }
}
