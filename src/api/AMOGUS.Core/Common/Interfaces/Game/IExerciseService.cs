
using AMOGUS.Core.Domain.Models.ApiModels;
using AMOGUS.Core.Domain.Models.Game;

namespace AMOGUS.Core.Common.Interfaces.Game {
    public interface IExerciseService {

        Task<List<Exercise>> GetRandomExercisesAsync(int amount);

        Task<bool> CheckAnswer(Exercise answer);
    }
}
