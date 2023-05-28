
using AMOGUS.Core.Common.Interfaces.Configuration;
using AMOGUS.Core.Common.Interfaces.Game;
using AMOGUS.Core.Domain.Enums;
using AMOGUS.Core.Domain.Models.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AMOGUS.UnitTests")]
[assembly: InternalsVisibleTo("AMOGUS.Benchmarks"), InternalsVisibleTo("DynamicProxyGenAssembly2, PublicKey=0024000004800000940000000602000000240000525341310004000001000100c547cac37abd99c8db225ef2f6c8a3602f3b3606cc9891605d02baa56104f4cfc0734aa39b93bf7852f7d9266654753cc297e7d2edfe0bac1cdcf9f717241550e0a7b191195b7667bb4f64bcb8e2121380fd1d9d46ad2d92d2d15605093924cceaf74c4861eff62abf69b9291ed0a340e113be11e6a7d3113e92484cf7045cc7")]
namespace AMOGUS.Core.Services.Gameplay { 
    [ExcludeFromCodeCoverage]
    internal class QuestionFileAccessor : IQuestionFileAccessor {

        private readonly IQuestionRepoConfiguration _questionRepoConfiguration;

        private readonly string _exercisePath;
        private readonly string _exerciseExtension = ".amex";

#pragma warning disable 8618 // Value cannot be null after constructor
        private static List<Question> _questions;
#pragma warning restore 8618

        private readonly ILogger<QuestionFileAccessor> _logger;

        public QuestionFileAccessor(IQuestionRepoConfiguration questionRepoConfiguration, ILogger<QuestionFileAccessor> logger) {
            _questionRepoConfiguration = questionRepoConfiguration!;
            _exercisePath = _questionRepoConfiguration.ExercisePath;
            _logger = logger!;

            if (_questions is null) {
                ReloadQuestions();
            }
        }

        public void ReloadQuestions() {
            if (_logger.IsEnabled(LogLevel.Information)) {
                _logger.LogInformation("Loading questions from files at {dir}", _exercisePath);
            }
            _questions = new List<Question>();

            List<string> files = Directory.GetFiles(_exercisePath)
                .Where(e => Path.GetExtension(e).Equals(_exerciseExtension))
                .ToList();
            foreach (var f in files) {
                var convertedQuestions = JsonConvert.DeserializeObject<List<Question>>(File.ReadAllText(f));

                if (_logger.IsEnabled(LogLevel.Information)) {
                    _logger.LogInformation("Loaded question {q}", convertedQuestions?.Select(x => x.ToString() + "\n"));
                }
                foreach (var q in convertedQuestions!) {
                    _questions.Add(q);
                }
            }
        }

        public Question? Find(Predicate<Question> predicate) {
            return _questions.Find(predicate);
        }

        public List<Question> GetAllQuestions() {
            return _questions;
        }

        public List<Question> GetAllQuestionsByCategory(CategoryType category) {
            return _questions.Where(e => e.Category == category).ToList();
        }

        public List<Question> GetRandomQuestionsByCategory(CategoryType category, int amount) {
            var rng = new Random();
            return _questions.Where(e => e.Category == category).OrderBy(e => rng.Next()).Take(amount).ToList();
        }

        public async Task SaveQuestionFilesAsync() {
            var categories = GetAllCategories();
            foreach (var category in categories) {
                var path = GetQuestionFilenameByCategory(category);
                var quest = GetAllQuestionsByCategory(category);
                var json = JsonConvert.SerializeObject(quest);
                await File.WriteAllTextAsync(path, json);
            }
        }

        public string GetQuestionFilenameByCategory(CategoryType category) {
            return Path.Combine(_exercisePath, $"{category.ToString().ToLower()}questions{_exerciseExtension}");
        }

        public CategoryType[] GetAllCategories() {
            return Enum.GetValues<CategoryType>();
        }
    }
}
