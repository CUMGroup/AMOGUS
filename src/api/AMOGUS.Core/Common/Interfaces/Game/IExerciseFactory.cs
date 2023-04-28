

using AMOGUS.Core.Domain.Models.Generators;

namespace AMOGUS.Core.Common.Interfaces.Game {
    public interface IExerciseFactory {

        MentalExerciseModel GenerateRandomExerciseString(bool insaneMode = false);

        string CalcAnswer(string question);

        MentalExerciseModel CalcXp(MentalExerciseModel expr);
    }
}
