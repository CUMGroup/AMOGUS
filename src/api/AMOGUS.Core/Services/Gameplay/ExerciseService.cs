using AMOGUS.Core.Common.Interfaces.Game;
using AMOGUS.Core.Domain.Enums;
using AMOGUS.Core.Domain.Models.Entities;
using AMOGUS.Core.Factories;
using AngouriMath;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AMOGUS.UnitTests")]
namespace AMOGUS.Core.Services.Gameplay {
    internal class ExerciseService : IExerciseService {

        private readonly IQuestionFileAccessor _questionFileAccessor;

        public ExerciseService(IQuestionFileAccessor questionFileAccessor) {
            _questionFileAccessor = questionFileAccessor!;
        }

        public bool CheckAnswer(Question answer) {
            var qOrig = _questionFileAccessor.Find(e => e.QuestionId.Equals(answer.QuestionId));
            if (qOrig is null)
                return false;
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
            return _questionFileAccessor.GetRandomQuestionsByCategory(category, amount);
        }


        public List<Question> GenerateRandomMentalExercises(int amount) {
            var factory = new MentalExerciseFactory();
            var questions = new List<Question>();
            for (int i = 0; i < amount; ++i) {
                var questString = factory.GenerateRandomExerciseString();
                var answString = factory.CalcAnswer(questString);
                if (String.IsNullOrWhiteSpace(answString)) {
                    --i;
                    continue;
                }
                questions.Add(new Question {
                    Category = CategoryType.MENTAL,
                    QuestionId = Guid.NewGuid().ToString(),
                    Exercise = questString,
                    Answer = answString,
                    Difficulty = DifficultyType.EASY,
                    ExperiencePoints = 5,
                    Help = string.Empty
                });
            }
            return questions;
        }
    }
}
