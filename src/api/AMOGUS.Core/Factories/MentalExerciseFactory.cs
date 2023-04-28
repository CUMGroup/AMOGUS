using AMOGUS.Core.Common.Interfaces.Game;
using AMOGUS.Core.Domain.Models.Generators;
using AngouriMath;
using AngouriMath.Core.Exceptions;

namespace AMOGUS.Core.Factories {
    public class MentalExerciseFactory : IExerciseFactory {

        private static readonly Random _rng = new();
        private const int numOperandsMaxXp = 20;
        private const int avgOperandsMaxXp = 5;
        private const int avgOperatorsMaxXp = 5;
        private const int answerMaxXp = 600;

        private const int sumMaxXp = numOperandsMaxXp + avgOperandsMaxXp + avgOperatorsMaxXp + answerMaxXp;


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

        public MentalExerciseModel GenerateRandomExerciseString(bool insaneMode = false) {
            var numOperands = insaneMode ?
        GenerateIntWithFallingProbability(3, 7)
        : GenerateIntWithFallingProbability(2, 5);

            var expr = new MentalExerciseModel {
                Operands = new int[numOperands],
                Operators = new int[numOperands - 1],
                MinOperand = 5,
                MaxOperand = 40
            };

            expr.Expression += expr.Operands[^numOperands] = _rng.Next(expr.MinOperand, expr.MaxOperand);
            numOperands--;

            while (numOperands > 0) {

                var signRng = insaneMode ?
                    _rng.Next(3)
                    : GenerateIntWithFallingProbability(0, 3);

                expr.Operators[^numOperands] = signRng;

                switch (signRng) {
                    case 0:
                        expr.Expression += " + ";
                        break;
                    case 1:
                        expr.Expression += " - ";
                        break;
                    case 2:
                        expr.Expression += " * ";
                        break;
                }
                byte tryCount = 0;
                do {
                    expr.Operands[^numOperands] = _rng.Next(expr.MinOperand, Math.Max(expr.MaxOperand - 20, 10));
                    ++tryCount;
                } while (insaneMode && tryCount <= 3 && expr.Operands[^numOperands] % 10 == 0);
                expr.Expression += expr.Operands[^numOperands];
                numOperands--;
            }
            return expr;
        }


        public MentalExerciseModel CalcXp(MentalExerciseModel expr) {
            if (expr.Answer == null) {
                throw new ArgumentException("No answer given");
            }
            if (Math.Abs((int) expr.Answer) > 10_000) {
                throw new ArgumentException("Answer is too big");
            }

            int amountMod10 = expr.Operands.Select(e => e % 10 == 0 ? 1 : 0).Sum() + 1;
            int amountMod5 = expr.Operands.Select(e => e % 5 == 0 ? 1 : 0).Sum() + 1 - amountMod10;

            double numOperandsNormalized = (expr.Operands.Length - 2) / 6d;
            double avgOperandsNormalized = Median(expr.Operands) / expr.MaxOperand;
            double avgOperatorsNormalized = Median(expr.Operators) / 2;
            double answerNormalized = Math.Abs((int) expr.Answer) / 10_000d;

            expr.Xp = (int) (((numOperandsMaxXp * numOperandsNormalized
                + avgOperandsMaxXp * avgOperandsNormalized
                + avgOperatorsMaxXp * avgOperatorsNormalized
                + answerMaxXp * answerNormalized) / (double) Math.Max((amountMod5 - (expr.Operands.Length - amountMod5 + 1)), 1)) / (double) amountMod10);

            expr.Difficulty = (int) Math.Round(-Math.Exp((-4d / sumMaxXp) * (int) expr.Xp + 1.4) + 4);

            return expr;
        }

        /*
         * This generates a random number in the range [min, max) (exclusive)
         * 
         * It was tested with random numbers in [2, 6) and yielded following probability distribution. (100_000_000 tries)
         * 
         *  2 : 70% (70709718 Hits)
         *  3 : 13% (13381646 Hits)
         *  4 :  8% (8969867 Hits)
         *  5 :  6% (6938769 Hits)
         */
        private static int GenerateIntWithFallingProbability(int min, int max) {
            // make max exclusive
            max -= 1;
            if (min > max)
                throw new ArgumentException("Min cannot be greater than max");
            if (min == max)
                return min;

            int range = max - min + 1;
            double rand = _rng.NextDouble();

            double adjustedRandomNumber = Math.Pow(rand, 4);
            int generated = (int) (min + (adjustedRandomNumber * range));
            return generated;
        }

        private static double Median(int[] arr) {
            Array.Sort(arr);
            double mid = arr.Length / 2d;
            if (mid == (int) mid)
                return arr[(int) mid];
            return (arr[(int) mid] + arr[Math.Min((int) mid + 1, arr.Length - 1)]) / 2d;
        }
    }
}
