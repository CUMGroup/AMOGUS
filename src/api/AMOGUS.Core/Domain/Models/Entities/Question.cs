
using AMOGUS.Core.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace AMOGUS.Core.Domain.Models.Entities {
    public class Question {

        [Key]
        public string QuestionId { get; set; }
        
        public string Exercise { get; set; }
        public string Answer { get; set; }
        public string Help { get; set; }
        public DifficultyType Difficulty { get; set; }
        public CategoryType Category { get; set; }
        public int ExperiencePoints { get; set; }


        // n to m relationship to the GameSessions
        public ICollection<GameSession> Sessions { get; set; }

    }
}
