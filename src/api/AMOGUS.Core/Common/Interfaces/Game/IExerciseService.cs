
using AMOGUS.Core.Domain.Enums;
using AMOGUS.Core.Domain.Models.Entities;

namespace AMOGUS.Core.Common.Interfaces.Game {
    public interface IExerciseService {

        List<Question> GetRandomExercises(CategoryType category, int amount);

        Task<bool> CheckAnswerAsync(Question answer);

        List<Question> GenerateRandomExercises(CategoryType category, int amount);
    }
}
