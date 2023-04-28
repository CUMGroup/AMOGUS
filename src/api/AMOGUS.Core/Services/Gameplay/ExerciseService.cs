using AMOGUS.Core.Common.Interfaces.Game;
using AMOGUS.Core.Domain.Enums;
using AMOGUS.Core.Domain.Models.Entities;
using AMOGUS.Core.Factories;
using AngouriMath;

namespace AMOGUS.Core.Services.Gameplay {
    internal class ExerciseService : IExerciseService {

        private readonly IQuestionFileAccessor _questionFileAccessor;

        private IExerciseFactory? _exerciseFactory;
        public IExerciseFactory ExerciseFactory { 
            get {
                return _exerciseFactory ??= new MentalExerciseFactory();
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
                origAnswer = new MentalExerciseFactory().CalcAnswer(answer.Exercise);
                if (String.IsNullOrWhiteSpace(origAnswer))
                    return false;
            }

            try {
                if (!(answer.Category == CategoryType.MENTAL || isRandomMental)) {
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

        public List<Question> GetRandomExercises(CategoryType category, int amount) {
            if(category == CategoryType.RANDOMMENTAL || category == CategoryType.RANDOMMENTAL_INSANE) {
                return GenerateRandomMentalExercises(amount, category == CategoryType.RANDOMMENTAL_INSANE);
            }
            return _questionFileAccessor.GetRandomQuestionsByCategory(category, amount);
        }


        public List<Question> GenerateRandomMentalExercises(int amount, bool insaneMode) {
            var factory = ExerciseFactory;
            var questions = new List<Question>();
            for (int i = 0; i < amount; ++i) {
                questions.Add(factory.GenerateRandomQuestion(insaneMode));
            }
            return questions;
        }
    }
}
