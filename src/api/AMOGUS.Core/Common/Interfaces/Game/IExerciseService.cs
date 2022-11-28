
using AMOGUS.Core.Domain.Enums;
using AMOGUS.Core.Domain.Models.Entities;

namespace AMOGUS.Core.Common.Interfaces.Game {
    public interface IExerciseService {

        Task<List<Question>> GetRandomExercisesAsync(CategoryType category, int amount);

        Task<bool> CheckAnswerAsync(Question answer);
    }
}
