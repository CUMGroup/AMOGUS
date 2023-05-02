

using AMOGUS.Core.Domain.Models.Entities;
using AMOGUS.Core.Domain.Models.Generators;

namespace AMOGUS.Core.Common.Interfaces.Game {
    public interface IExerciseFactory {

        Question GenerateRandomQuestion(bool insaneMode);

        MentalExerciseModel GenerateRandomExerciseModel(bool insaneMode);

        string CalcAnswer(string question);

        MentalExerciseModel CalcXp(MentalExerciseModel expr);
    }
}
