
using Microsoft.Extensions.Configuration;

namespace AMOGUS.Core.Common.Interfaces.Configuration {
    public interface IQuestionRepoConfiguration {

        string ExercisePath { get; }

    }

    internal class QuestionRepoConfiguration : IQuestionRepoConfiguration {

        private readonly IConfiguration _configuration;

        public QuestionRepoConfiguration(IConfiguration configuration) {
            _configuration = configuration;
        }

        public string ExercisePath => _configuration["ExercisePath"]!;
    }
}
