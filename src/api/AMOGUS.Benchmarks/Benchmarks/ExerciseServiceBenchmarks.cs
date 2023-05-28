using AMOGUS.Core.Common.Interfaces.Configuration;
using AMOGUS.Core.Domain.Enums;
using AMOGUS.Core.Domain.Models.Entities;
using AMOGUS.Core.Factories;
using AMOGUS.Core.Services.Gameplay;
using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.Logging;
using Moq;

namespace AMOGUS.Benchmarks.Benchmarks {
    [MemoryDiagnoser(false)]
    public class ExerciseServiceBenchmarks {

        #region Setup

        private ExerciseService _exerciseService;

        [Params(1, 10, 100)]
        public int Amount { get; set; }

        private List<Question> _randomMentalQuestions;
        private List<Question> _randomAnalysisQuestions;
        private List<Question> _randomGeometryQuestions;

        private List<Question> _randomRandomMentalInsaneQuestions;
        private List<Question> _randomRandomMentalQuestions;

        [GlobalSetup]
        public void GlobalSetup() {
            Mock<IQuestionRepoConfiguration> questionRepoConfigMock = new();
            questionRepoConfigMock
                .Setup(e => e.ExercisePath)
                .Returns("../../../../../../../../AMOGUS.Api/Content");
            Mock<ILogger<QuestionFileAccessor>> loggerMock = new();
            loggerMock
                .Setup(e => e.IsEnabled(It.IsAny<LogLevel>()))
                .Returns(false);

            var questionFileAccessor = new QuestionFileAccessor(questionRepoConfigMock.Object, loggerMock.Object);
            var exerciseFactory = new MentalExerciseFactory();
            _exerciseService = new ExerciseService(questionFileAccessor) {
                ExerciseFactory = exerciseFactory
            };

            _randomAnalysisQuestions = _exerciseService.GetRandomExercises(CategoryType.ANALYSIS, Amount);
            _randomMentalQuestions = _exerciseService.GetRandomExercises(CategoryType.MENTAL, Amount);
            _randomGeometryQuestions = _exerciseService.GetRandomExercises(CategoryType.GEOMETRY, Amount);

            _randomRandomMentalQuestions = _exerciseService.GetRandomExercises(CategoryType.RANDOMMENTAL, Amount);
            _randomRandomMentalInsaneQuestions = _exerciseService.GetRandomExercises(CategoryType.RANDOMMENTAL_INSANE, Amount);
        }

        #endregion

        #region Correct Answers
        [Benchmark]
        public void CheckAnswer_For_Mental_Correct() {
            if (Amount > 40) {
                return; // Skip Benchmarks -> There are not enough questions in the catalogue
            }

            foreach (var quest in _randomMentalQuestions) {
                _exerciseService.CheckAnswer(quest);
            }
        }

        [Benchmark]
        public void CheckAnswer_For_Analysis_Correct() {
            if (Amount > 40) {
                return; // Skip Benchmarks -> There are not enough questions in the catalogue
            }
            foreach (var quest in _randomAnalysisQuestions) {
                _exerciseService.CheckAnswer(quest);
            }
        }

        [Benchmark]
        public void CheckAnswer_For_Geometry_Correct() {
            if (Amount > 40) {
                return; // Skip Benchmarks -> There are not enough questions in the catalogue
            }
            foreach (var quest in _randomGeometryQuestions) {
                _exerciseService.CheckAnswer(quest);
            }
        }

        [Benchmark]
        public void CheckAnswer_For_RandomMental_Correct() {
            foreach (var quest in _randomRandomMentalQuestions) {
                _exerciseService.CheckAnswer(quest);
            }
        }

        [Benchmark]
        public void CheckAnswer_For_RandomMentalInsane_Correct() {
            foreach (var quest in _randomRandomMentalInsaneQuestions) {
                _exerciseService.CheckAnswer(quest);
            }
        }

        #endregion

        #region Wrong Answers

        [Benchmark]
        public void CheckAnswer_For_Mental_Wrong() {
            if (Amount > 40) {
                return; // Skip Benchmarks -> There are not enough questions in the catalogue
            }

            foreach (var quest in _randomMentalQuestions) {
                quest.Answer = "Wrong answer";
                _exerciseService.CheckAnswer(quest);
            }
        }

        [Benchmark]
        public void CheckAnswer_For_Analysis_Wrong() {
            if (Amount > 40) {
                return; // Skip Benchmarks -> There are not enough questions in the catalogue
            }
            foreach (var quest in _randomAnalysisQuestions) {
                quest.Answer = "Wrong answer";
                _exerciseService.CheckAnswer(quest);
            }
        }

        [Benchmark]
        public void CheckAnswer_For_Geometry_Wrong() {
            if (Amount > 40) {
                return; // Skip Benchmarks -> There are not enough questions in the catalogue
            }
            foreach (var quest in _randomGeometryQuestions) {
                quest.Answer = "Wrong answer";
                _exerciseService.CheckAnswer(quest);
            }
        }

        [Benchmark]
        public void CheckAnswer_For_RandomMental_Wrong() {
            foreach (var quest in _randomRandomMentalQuestions) {
                quest.Answer = "2.3";
                _exerciseService.CheckAnswer(quest);
            }
        }

        [Benchmark]
        public void CheckAnswer_For_RandomMentalInsane_Wrong() {
            foreach (var quest in _randomRandomMentalInsaneQuestions) {
                quest.Answer = "2.3";
                _exerciseService.CheckAnswer(quest);
            }
        }

        #endregion
    }
}
