using AMOGUS.Core.Common.Interfaces.Game;
using AMOGUS.Core.Domain.Models.Entities;
using Microsoft.Extensions.Configuration;
using AMOGUS.Core.ExtensionMethods;
using AMOGUS.Core.Domain.Enums;
using Newtonsoft.Json;
using AngouriMath;
using AngouriMath.Core.Exceptions;
using AMOGUS.Core.Factories;

namespace AMOGUS.Core.Services.Gameplay {
    internal class ExerciseService : IExerciseService {

        private readonly string _exercisePath;
        private readonly string _exerciseExtension = ".amex";

        private static List<Question> _questions;

        public ExerciseService(IConfiguration configuration) {
            _exercisePath = configuration.GetExercisePathString();
            
            if(_questions is null) {
                Task.Run(async () => {
                    await ReloadQuestionsAsync();
                });
            }
        }

        public async Task ReloadQuestionsAsync() {
            _questions = new();

            List<string> files = Directory.GetFiles(_exercisePath)
                .Where(e => Path.GetExtension(e).Equals(_exerciseExtension))
                .ToList();
            foreach(var f in files) {
                _questions.AddRange(JsonConvert.DeserializeObject<List<Question>>(await File.ReadAllTextAsync(f)) ! );
            }
        }

        public async Task<bool> CheckAnswerAsync(Question answer) {
            var qOrig = _questions.Find(e => e.QuestionId.Equals(answer.QuestionId));
            if (qOrig is null)
                return false;
            Entity exprTrue = qOrig.Answer;
            Entity exprUser = answer.Answer;
            try {
                return new Entity.Equalsf(exprTrue, exprUser).Simplify().EvalBoolean();
            }catch(CannotEvalException) {
                return false;
            }
        }

        public List<Question> GetRandomExercises(CategoryType category, int amount) {
            var rng = new Random();
            return _questions.Where(e => e.Category == category).OrderBy(e => rng.Next()).Take(amount).ToList();
        }


        public List<Question> GenerateRandomExercises(CategoryType category, int amount) {
            var factory = GetExerciseFactory(category);
            var questions = new List<Question>();
            for (int i = 0; i < amount; ++i) {
                var questString = factory.GenerateRandomExerciseString();
                var answString = factory.CalcAnswer(questString);
                if(String.IsNullOrWhiteSpace(answString)) {
                    --i;
                    continue;
                }
                questions.Add(new Question {
                    Category = category,
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

        private IExerciseFactory GetExerciseFactory(CategoryType category) {
            return category switch {
                CategoryType.MENTAL => new MentalExerciseFactory(),
                _ => throw new ArgumentException(),
            };
        }
    }
}
