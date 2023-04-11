using AMOGUS.Core.Common.Interfaces.Game;
using AMOGUS.Core.Domain.Enums;
using AMOGUS.Core.Domain.Models.Entities;
using AMOGUS.Core.ExtensionMethods;
using AngouriMath;
using AngouriMath.Core.Exceptions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace AMOGUS.Core.Services.Gameplay {
    internal class ExerciseService : IExerciseService {

        private readonly string _exercisePath;
        private readonly string _exerciseExtension = ".amex";

        private static List<Question> _questions;

        public ExerciseService(IConfiguration configuration) {
            _exercisePath = configuration.GetExercisePathString();

            if (_questions is null) {
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
            foreach (var f in files) {
                _questions.AddRange(JsonConvert.DeserializeObject<List<Question>>(await File.ReadAllTextAsync(f))!);
            }
        }

        public async Task<bool> CheckAnswerAsync(Question answer) {
            var qOrig = _questions.Find(e => e.QuestionId.Equals(answer.QuestionId));
            if (qOrig is null)
                return false;
            Entity exprTrue = qOrig.Answer;
            Entity exprUser = answer.Answer;
            try {
                var res = new Entity.Equalsf(exprTrue, exprUser).Simplify().EvalBoolean();
                return res;
            }
            catch (CannotEvalException) {
                return false;
            }
        }

        public async Task<List<Question>> GetRandomExercisesAsync(CategoryType category, int amount) {
            var rng = new Random();
            return _questions.Where(e => e.Category == category).OrderBy(e => rng.Next()).Take(amount).ToList();
        }
    }
}
