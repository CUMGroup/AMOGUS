

using AMOGUS.Core.Domain.Models.Entities;

namespace AMOGUS.Core.Common.Interfaces.Game {
    public interface IExerciseFactory {

        Question GenerateRandomQuestion(bool insaneMode);

        string CalcAnswer(string question);
    }
}
