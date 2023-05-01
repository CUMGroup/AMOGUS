using AMOGUS.Core.Domain.Enums;
using AMOGUS.Infrastructure.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMOGUS.Core.Domain.Models.Entities {
    public class GameSession {

        [Key]
        public string SessionId { get; set; } = string.Empty;

        [ForeignKey("User")]
        public string UserId { get; set; } = string.Empty;

        public ApplicationUser? User { get; set; }

        public double Playtime { get; set; }

        public int CorrectAnswersCount { get; set; }

        public int GivenAnswersCount { get; set; }

        public double AverageTimePerQuestion { get; set; }

        public double QuickestAnswer { get; set; }

        public double SlowestAnswer { get; set; }

        public CategoryType Category { get; set; }

        public DateTime PlayedAt { get; set; }

        [NotMapped]
        public List<Question> Questions { get; set; } = new();

    }
}
