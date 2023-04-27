﻿
using AMOGUS.Core.Common.Interfaces.Configuration;
using AMOGUS.Core.Common.Interfaces.Game;
using AMOGUS.Core.Domain.Enums;
using AMOGUS.Core.Domain.Models.Entities;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AMOGUS.UnitTests")]
namespace AMOGUS.Core.Services.Gameplay {
    internal class QuestionFileAccessor : IQuestionFileAccessor {

        private readonly IQuestionRepoConfiguration _questionRepoConfiguration;

        private readonly string _exercisePath;
        private readonly string _exerciseExtension = ".amex";

        private static List<Question> _questions;


        public QuestionFileAccessor(IQuestionRepoConfiguration questionRepoConfiguration) {
            _questionRepoConfiguration = questionRepoConfiguration!;
            _exercisePath = _questionRepoConfiguration.ExercisePath;

            if (_questions is null) {
                ReloadQuestionsAsync().Wait();
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
