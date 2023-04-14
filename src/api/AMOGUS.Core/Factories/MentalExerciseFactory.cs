using AMOGUS.Core.Common.Interfaces.Game;
using AngouriMath;
using AngouriMath.Core.Exceptions;

namespace AMOGUS.Core.Factories {
    public class MentalExerciseFactory : IExerciseFactory {
        public string CalcAnswer(string question) {
            if (question is null)
                return string.Empty;
            Entity expr = question;
            try {
                if (question.Contains("x")) {
                    var set = expr.Solve("x");
                    if (set is Entity.Set.FiniteSet finiteset)
                        expr = finiteset.First();
                    expr = set;
                }
                return expr.Simplify().ToString();
            }
            catch (CannotEvalException) {
                return string.Empty;
            }
        }

        public string GenerateRandomExerciseString() {
            var rng = new Random();
            var quest = "";

            var signRng = rng.NextDouble();
            var sign = signRng > .5 ? (signRng > .75 ? '+' : '-') : (signRng < .25 ? '/' : '*');
            if (sign == '+' || sign == '-') {
                quest += rng.NextDouble() * 250;
                quest += sign;
                quest += rng.NextDouble() * 250;
            }
            else if (sign == '*') {
                quest += rng.NextDouble() * 20;
                quest += sign;
                quest += rng.NextDouble() * 10;
            }
            else {
                quest += rng.NextDouble() * 50;
                quest += sign;
                quest += rng.NextDouble() * 10;
            }
            return quest;
        }
    }
}
