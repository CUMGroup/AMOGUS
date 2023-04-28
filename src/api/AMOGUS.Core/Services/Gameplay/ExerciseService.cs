using AMOGUS.Core.Common.Interfaces.Game;
using AMOGUS.Core.Domain.Enums;
using AMOGUS.Core.Domain.Models.Entities;
using AMOGUS.Core.Factories;
using AngouriMath;

namespace AMOGUS.Core.Services.Gameplay {
    internal class ExerciseService : IExerciseService {

        private readonly IQuestionFileAccessor _questionFileAccessor;

        public ExerciseService(IQuestionFileAccessor questionFileAccessor) {
            _questionFileAccessor = questionFileAccessor!;
        }

        public bool CheckAnswer(Question answer) {
            var qOrig = _questionFileAccessor.Find(e => e.QuestionId.Equals(answer.QuestionId));
            if (qOrig is null)
                return false; // maybe a random question? -> Test!
            try {
                if (!answer.Category.Equals(CategoryType.MENTAL)) {
                    return qOrig.Answer.Equals(answer.Answer);
                }
                Entity exprTrue = qOrig.Answer;
                Entity exprUser = answer.Answer;
                return new Entity.Equalsf(exprTrue, exprUser).Simplify().EvalBoolean();
            }
            catch (Exception) {
                return qOrig.Answer.Equals(answer.Answer);
            }
        }

        public List<Question> GetRandomExercises(CategoryType category, int amount) {
            if(category == CategoryType.RANDOMMENTAL || category == CategoryType.RANDOMMENTAL_INSANE) {
                return GenerateRandomMentalExercises(amount, category == CategoryType.RANDOMMENTAL_INSANE);
            }
            return _questionFileAccessor.GetRandomQuestionsByCategory(category, amount);
        }


        public List<Question> GenerateRandomMentalExercises(int amount, bool insaneMode) {
            var factory = new MentalExerciseFactory();
            var questions = new List<Question>();
            for (int i = 0; i < amount; ++i) {
                questions.Add(factory.GenerateRandomQuestion(insaneMode));
            }
            return questions;
        }
    }
}
