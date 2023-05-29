using AMOGUS.Core.Common.Interfaces.Game;
using AMOGUS.Core.Domain.Enums;
using AMOGUS.Core.Domain.Models.Entities;
using AMOGUS.Core.Domain.Models.Generators;
using AngouriMath;
using AngouriMath.Core.Exceptions;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AMOGUS.UnitTests")]
[assembly: InternalsVisibleTo("AMOGUS.Benchmarks")]
namespace AMOGUS.Core.Factories {

    public class MentalExerciseFactory : IExerciseFactory {

        private const int maxGenerationTryCount = 10;

        private static readonly Random _rng = new();
        private const int numOperandsMaxXp = 20;
        private const int avgOperandsMaxXp = 5;
        private const int avgOperatorsMaxXp = 5;
        private const int answerMaxXp = 600;

        private const int sumMaxXp = numOperandsMaxXp + avgOperandsMaxXp + avgOperatorsMaxXp + answerMaxXp;

        [ExcludeFromCodeCoverage]
        public Question GenerateRandomQuestion(bool insaneMode) {

            int tryCount = 0;
            while (tryCount++ < maxGenerationTryCount) {

                var expr = GenerateRandomExerciseModel(insaneMode);
                var ansString = CalcAnswer(expr.Expression);

                if (String.IsNullOrWhiteSpace(ansString))
                    continue;

                bool succ = int.TryParse(ansString, out int ans);
                if (!succ)
                    continue;

                expr.Answer = ans;

                try {
                    expr = CalcXp(expr);
                    if (expr.Difficulty is null)
                        continue;
                    if (expr.Xp is null)
                        continue;

                    return ExpressionModelToQuestion(expr, insaneMode);
                }
                catch (ArgumentException) {
                    continue;
                }
            }
            // Should be pretty much unreachable
            return DummyQuestion(insaneMode);
        }

        public string CalcAnswer(string question) {
            if (String.IsNullOrWhiteSpace(question))
                return string.Empty;
            Entity expr = question;
            try {
                if (question.Contains('x')) {
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
            catch (SolveRequiresStatementException) {
                return string.Empty;
            }
        }

        private MentalExerciseModel GenerateRandomExerciseModel(bool insaneMode = false) {
            var numOperands = GetRandomNumberOfOperands(insaneMode);

            var expr = new MentalExerciseModel {
                Operands = new int[numOperands],
                Operators = new int[numOperands - 1],
                MinOperand = 5,
                MaxOperand = 40
            };

            expr.Expression += expr.Operands[^numOperands] = _rng.Next(expr.MinOperand, expr.MaxOperand);
            --numOperands;

            while (numOperands > 0) {

                var signRng = GetRandomOperator(insaneMode);

                expr.Operators[^numOperands] = signRng;
                expr.Expression += OperatorToSignString(signRng);
                
                byte tryCount = 0;
                do {
                    expr.Operands[^numOperands] = _rng.Next(expr.MinOperand, Math.Max(expr.MaxOperand - 20, 10));
                    ++tryCount;

                // if insaneMode -> Try to reduce operands that are divisible by 10
                } while (insaneMode && tryCount <= 3 && expr.Operands[^numOperands] % 10 == 0);
                expr.Expression += expr.Operands[^numOperands];
                numOperands--;
            }
            return expr;
        }

        /** <summary>
         * RandomMentalMode: between 2 and 4 operands<br></br>
         * RandomMentalInsaneMode: between 3 and 6 operands
         * </summary>
         */
        private int GetRandomNumberOfOperands(bool insaneMode) {
            
            return insaneMode ?
                GenerateIntWithFallingProbability(3, 7)
                : GenerateIntWithFallingProbability(2, 5);
        }

        /** <summary>
         *  RandomMentalMode: +, -, * with falling probability <br></br>
         *  RandomMentalInsaneMode: +, -, * with equal probability
         * </summary>
         */
        private int GetRandomOperator(bool insaneMode) {
            return insaneMode ?
                    _rng.Next(3)
                    : GenerateIntWithFallingProbability(0, 3);
        }

        /** <summary>
         * Maps:<br></br>
         * 0: ' + ' <br></br>
         * 1: ' - ' <br></br>
         * 2: ' * ' <br></br>
         * default: ' / '
         * </summary>
         */
        private string OperatorToSignString(int op) {
            return op switch {
                0 => " + ",
                1 => " - ",
                2 => " * ",
                _ => " / ",
            };
        }

        /**<summary>
         * Xp-Calculation (latex):<br></br>
         * \text{xp}=Max( \left(\frac{\begin{pmatrix} \text{numOperandsMaxXp} \\ \text{avgOperandsMaxXp} \\ \text{avgOperatorsMaxXp} \\ \text{answerMaxXp} \end{pmatrix}^\top \cdot \begin{pmatrix} \frac{\text{numOperands}-2}{6} \\ \frac{Median(\text{Operands})}{40} \\ \frac{Median(\text{Operators})}{2} \\ \frac{|\text{Answer}|}{10000} \end{pmatrix}}{Max((\text{amountMod5} - \text{amountNotMod5} - 1), 1) \cdot (\text{amountMod10} + 1)}\right), 0)
         * </summary>
         */
        private MentalExerciseModel CalcXp(MentalExerciseModel expr) {
            if (expr.Answer == null) {
                throw new ArgumentException("No answer given");
            }
            if (Math.Abs((int) expr.Answer) > 10_000) {
                throw new ArgumentException("Answer is too big");
            }

            int amountMod10 = CountElementsDivisibleBy(expr.Operands, 10);
            // all mod 5s are also mod 10s -> so exclude the mod 10s
            int amountMod5 = CountElementsDivisibleBy(expr.Operands, 5) - amountMod10;
            int amountNotMod5 = expr.Operands.Length - amountMod5;

            double numOperandsNormalized = (expr.Operands.Length - 2) / 6d;
            double avgOperandsNormalized = Median(expr.Operands) / expr.MaxOperand;
            double avgOperatorsNormalized = Median(expr.Operators) / 2;
            double answerNormalized = Math.Abs((int) expr.Answer) / 10_000d;

            double weightedParams = 
                numOperandsMaxXp * numOperandsNormalized
                + avgOperandsMaxXp * avgOperandsNormalized
                + avgOperatorsMaxXp * avgOperatorsNormalized
                + answerMaxXp * answerNormalized;
 
            double modWeight = 1d / Math.Max(amountMod5 - amountNotMod5 - 1, 1) * (amountMod10 + 1);

            expr.Xp = Math.Max((int) ( weightedParams *  modWeight), 0);

            expr.Difficulty = CalculateDifficultyForXp((int)expr.Xp);

            return expr;
        }

        /**<summary>
         * Counts the number of elements that are divisible by 10
         * </summary>
         */
        internal int CountElementsDivisibleBy(IEnumerable<int> collection, int divisibleBy) {
            return collection.Select(e => e % divisibleBy == 0 ? 1 : 0).Sum();
        }

        /** <summary>
         * Calculates the Difficulty [0, 4] relative to a given xp amount.<br></br>
         * Latex: clamp(\lfloor-e^{\frac{-4}{\text{sumMaxXp}}\cdot\text{xp}+1.4}+4\rceil, 0, 4) <br></br>
         * Graph: https://www.desmos.com/calculator/tkgwnw0fkm
         * </summary>
         */
        private int CalculateDifficultyForXp(int xp) {
            double functionValue = - Math.Exp(-4d / sumMaxXp * xp + 1.4) + 4;
            int roundedValue = (int)Math.Round(functionValue);
            return Math.Clamp(roundedValue, 0, 4);
        }

        /**
         * <summary>
         * This generates a random number in the range [min, max) (exclusive).
         * <br></br>
         * It was tested with random numbers in [2, 6) and yielded following probability distribution. (100_000_000 tries)
         * <br></br>
         *  2 : 70% (70709718 Hits)<br></br>
         *  3 : 13% (13381646 Hits)<br></br>
         *  4 :  8% (8969867 Hits)<br></br>
         *  5 :  6% (6938769 Hits)<br></br>
         *  </summary>
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

        internal static double Median(int[] arr) {
            if (arr.Length == 0)
                return 0;
            if (arr.Length == 1)
                return arr[0];
            int[] sortedArr = (int[])arr.Clone();
            Array.Sort(sortedArr);
            int mid = sortedArr.Length / 2;
            if (sortedArr.Length % 2 != 0)
                return sortedArr[mid];
            return (sortedArr[mid] + sortedArr[Math.Max(Math.Min(mid - 1, sortedArr.Length - 1), 0)]) / 2d;
        }

        private static Question DummyQuestion(bool insaneMode) {
            return new Question {
                Answer = "37",
                Category = insaneMode ? CategoryType.RANDOMMENTAL_INSANE : CategoryType.RANDOMMENTAL,
                Difficulty = DifficultyType.EASY,
                Exercise = "30 + 7",
                ExperiencePoints = 4,
                QuestionId = "random"
            };
        }

        private static Question ExpressionModelToQuestion(MentalExerciseModel expr, bool insaneMode) {
            if(expr is null) {
                throw new ArgumentException("Expression cannot be null");
            }
            if(expr.Answer is null) {
                throw new ArgumentException("Answer cannot be null");
            }
            if (expr.Xp is null) {
                throw new ArgumentException("Xp cannot be null");
            }
            if (expr.Difficulty is null) {
                throw new ArgumentException("Difficulty cannot be null");
            }

            return new Question {
                Answer = ((int)expr.Answer).ToString(),
                Category = insaneMode ? CategoryType.RANDOMMENTAL_INSANE : CategoryType.RANDOMMENTAL,
                Difficulty = (DifficultyType) expr.Difficulty,
                Exercise = expr.Expression,
                ExperiencePoints = (int)expr.Xp,
                QuestionId = "random"
            };
        }
    }
}
