
using AMOGUS.Core.Common.Interfaces.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMOGUS.Core.Domain.Models.Game {
    public class UserStats {

        // TODO: Annotation

        [Key]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public IApplicationUser User { get; set; }

        public int Level { get; set; }
        public int ExperiencePoints { get; set; }
        public int CurrentStreak { get; set; }
        public int OverallAnswered { get; set; }
        public int CorrectAnswers { get; set; }
        public double TotalTimePlayed { get; set; }
        public double QuickestAnswer { get; set; }
        public double SlowestAnswer { get; set; }
        public int LongestStreak { get; set; }
    }
}
