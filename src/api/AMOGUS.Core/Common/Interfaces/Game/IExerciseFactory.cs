

namespace AMOGUS.Core.Common.Interfaces.Game {
    public interface IExerciseFactory {

        public string GenerateRandomExerciseString();

        public string CalcAnswer(string question);
    }
}
