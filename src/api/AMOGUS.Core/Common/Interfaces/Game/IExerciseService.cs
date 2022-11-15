
using AMOGUS.Core.Domain.Models.ApiModels;
using AMOGUS.Core.Domain.Models.Entities;

namespace AMOGUS.Core.Common.Interfaces.Game {
    public interface IExerciseService {

        Task<List<Question>> GetRandomExercisesAsync(int amount);

        Task<bool> CheckAnswerAsync(Question answer);
    }
}
