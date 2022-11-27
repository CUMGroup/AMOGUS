
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AMOGUS.Infrastructure.Identity;

namespace AMOGUS.Core.Domain.Models.Entities {
    public class GameSession {

        [Key]
        public string SessionId { get; set; }
        
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public TimeSpan Playtime { get; set; }
        public int CorrectAnswersCount { get; set; }
        public int GivenAnswersCount { get; set; }
        public TimeSpan AverageTimePerQuestion { get; set; }

        public TimeSpan QuickestAnswer { get; set; }

        public TimeSpan SlowestAnswer { get; set; }


        [NotMapped]
        public List<Question> Questions = new();

        [NotMapped]
        public double QuickestAnswerInMillis { get {
                return QuickestAnswer.TotalMilliseconds;
            } 
        }
        [NotMapped] 
        public double SlowestAnswerInSeconds { get {
                return SlowestAnswer.TotalSeconds;
            } 
        }


    }
}
