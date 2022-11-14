
using AMOGUS.Core.Common.Interfaces.User;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AMOGUS.Core.Domain.Models.Game {
    public class GameSession {

        [Key]
        public string SessionId { get; set; }
        
        [ForeignKey("User")]
        public string UserId { get; set; }
        public IApplicationUser User { get; set; }

        public TimeSpan Playtime { get; set; }
        public int CorrectAnswersCount { get; set; }
        public int GivenAnswersCount { get; set; }
        public TimeSpan AverageTimePerQuestion { get; set; }

        public TimeSpan QuickestAnswer { get; set; }

        public TimeSpan SlowestAnswer { get; set; }
        
        [NotMapped]
        public double QuickestAnswerInMillis { get {
                return QuickestAnswer.TotalMilliseconds;
            } 
        }

        public double SlowestAnswerInSeconds { get {
                return SlowestAnswer.TotalSeconds;
            } 
        }


    }
}
