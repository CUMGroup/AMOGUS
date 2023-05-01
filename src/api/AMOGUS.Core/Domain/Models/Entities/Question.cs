using AMOGUS.Core.Domain.Enums;

namespace AMOGUS.Core.Domain.Models.Entities {
    public class Question {

        public string QuestionId { get; set; } = string.Empty;

        public string Exercise { get; set; } = string.Empty;

        public string Answer { get; set; } = string.Empty;
        public List<string> WrongAnswers { get; set; } = new();

        public string? Help { get; set; }

        public DifficultyType Difficulty { get; set; }

        public CategoryType Category { get; set; }

        public int ExperiencePoints { get; set; }
    }
}
