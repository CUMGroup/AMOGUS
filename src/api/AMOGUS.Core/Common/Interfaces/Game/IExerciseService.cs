using AMOGUS.Core.Domain.Enums;
using AMOGUS.Core.Domain.Models.Entities;

namespace AMOGUS.Core.Common.Interfaces.Game {
    public interface IExerciseService {

        List<Question> GetRandomExercises(CategoryType category, int amount);

        bool CheckAnswer(Question answer);

        List<Question> GenerateRandomMentalExercises(int amount);
    }
}
