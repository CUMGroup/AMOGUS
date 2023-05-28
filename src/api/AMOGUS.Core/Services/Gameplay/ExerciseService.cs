using AMOGUS.Core.Common.Interfaces.Game;
using AMOGUS.Core.Domain.Enums;
using AMOGUS.Core.Domain.Models.Entities;
using AMOGUS.Core.Factories;
using AngouriMath;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AMOGUS.UnitTests")]
[assembly: InternalsVisibleTo("AMOGUS.Benchmarks")]
namespace AMOGUS.Core.Services.Gameplay {
    internal class ExerciseService : IExerciseService {

        private readonly IQuestionFileAccessor _questionFileAccessor;

        private IExerciseFactory? _exerciseFactory;
        public IExerciseFactory ExerciseFactory {
            get {
                _exerciseFactory ??= new MentalExerciseFactory();
                return _exerciseFactory;
            }
            set {
                _exerciseFactory = value;
            }
        }

        public ExerciseService(IQuestionFileAccessor questionFileAccessor) {
            _questionFileAccessor = questionFileAccessor!;
        }

        public bool CheckAnswer(Question answer) {
            bool isRandomMental = answer.Category == CategoryType.RANDOMMENTAL || answer.Category == CategoryType.RANDOMMENTAL_INSANE;

            var qOrig = _questionFileAccessor.Find(e => e.QuestionId.Equals(answer.QuestionId));
            string origAnswer = qOrig?.Answer ?? "";

            if (qOrig is null) {
                if (!isRandomMental)
                    return false;
                origAnswer = ExerciseFactory.CalcAnswer(answer.Exercise);
                if (String.IsNullOrWhiteSpace(origAnswer))
                    return false;
            }

            try {
                if (!isRandomMental) {
                    return origAnswer.Equals(answer.Answer);
                }
                Entity exprTrue = origAnswer;
                Entity exprUser = answer.Answer;
                return new Entity.Equalsf(exprTrue, exprUser).Simplify().EvalBoolean();
            }
            catch (Exception) {
                return origAnswer.Equals(answer.Answer);
            }
        }

        [ExcludeFromCodeCoverage]
        public List<Question> GetRandomExercises(CategoryType category, int amount) {
            if (category == CategoryType.RANDOMMENTAL || category == CategoryType.RANDOMMENTAL_INSANE) {
                return GenerateRandomMentalExercises(amount, category == CategoryType.RANDOMMENTAL_INSANE);
            }
            return _questionFileAccessor.GetRandomQuestionsByCategory(category, amount);
        }

        [ExcludeFromCodeCoverage]
        public List<Question> GenerateRandomMentalExercises(int amount, bool insaneMode) {
            var questions = new List<Question>();
            for (int i = 0; i < amount; ++i) {
                questions.Add(ExerciseFactory.GenerateRandomQuestion(insaneMode));
            }
            return questions;
        }
    }
}
