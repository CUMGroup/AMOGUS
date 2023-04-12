using AMOGUS.Core.Domain.Enums;

namespace AMOGUS.Core.Domain.Models.Entities {
    public class Question {

        public string QuestionId { get; set; }

        public string Exercise { get; set; }

        public string Answer { get; set; }
        public List<string> WrongAnswers { get; set; } = new();

        public string Help { get; set; }

        public DifficultyType Difficulty { get; set; }

        public CategoryType Category { get; set; }

        public int ExperiencePoints { get; set; }
    }
}
