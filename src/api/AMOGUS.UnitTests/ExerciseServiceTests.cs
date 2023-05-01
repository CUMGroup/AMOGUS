using AMOGUS.Core.Centralization.User;
using AMOGUS.Core.Common.Interfaces.Game;
using AMOGUS.Core.Domain.Enums;
using AMOGUS.Core.Domain.Models.Entities;
using AMOGUS.Core.Services.Gameplay;
using AMOGUS.Infrastructure.Services.User;
using Microsoft.AspNetCore.Identity;

namespace AMOGUS.UnitTests {
    public class ExerciseServiceTests {
        private Mock<IQuestionFileAccessor> CreateQuestionFileAccessorMock() => new();
        private Mock<IExerciseFactory> CreateExerciseFactoryMock() => new();

        #region CheckAnswer
        [Fact]
        public void CheckAnswer_WhenGivenAnswer_AndQuestionIsNotRandomMental_AndOrigQuestionNotFound_ReturnsFalse() {
            var questionFileAccessorMock = CreateQuestionFileAccessorMock();
            questionFileAccessorMock.Setup(x => x.Find(It.IsAny<Predicate<Question>>()))
                .Returns((Question) null);

            var exerciseService = new ExerciseService(questionFileAccessorMock.Object);

            var testAnswer = new Question() {
                QuestionId = "testID",
                Category = CategoryType.ANALYSIS
            };

            var result = exerciseService.CheckAnswer(testAnswer);

            Assert.False(result);
        }

        [Fact]
        public void CheckAnswer_WhenGivenAnswer_AndQuestionIsRandomMental_AndAnswerIsNotCalculable_ReturnsFalse() {
            var questionFileAccessorMock = CreateQuestionFileAccessorMock();
            questionFileAccessorMock.Setup(x => x.Find(It.IsAny<Predicate<Question>>()))
                .Returns((Question) null);

            var exerciseService = new ExerciseService(questionFileAccessorMock.Object);

            var testAnswer = new Question() {
                Exercise = "test exercise",
                Answer = "test answer",
                Category = CategoryType.RANDOMMENTAL
            };

            var result = exerciseService.CheckAnswer(testAnswer);

            Assert.False(result);
        }

        [Fact]
        public void CheckAnswer_WhenGivenAnswer_AndQuestionIsNotRandomMental_AndQuestionExists_ReturnAnswerIsCorrect() {
            var questionFileAccessorMock = CreateQuestionFileAccessorMock();
            questionFileAccessorMock.Setup(x => x.Find(It.IsAny<Predicate<Question>>()))
                .Returns(new Question() {
                    QuestionId = "test",
                    Exercise = "test exercise",
                    Answer = "correct answer"
                });

            var exerciseService = new ExerciseService(questionFileAccessorMock.Object);

            var testAnswer = new Question() {
                QuestionId = "test",
                Exercise = "test exercise",
                Answer = "correct answer",
                Category = CategoryType.ANALYSIS
            };

            var result = exerciseService.CheckAnswer(testAnswer);

            Assert.True(result);
        }

        [Fact]
        public void CheckAnswer_WhenGivenAnswer_AndQuestionIsNotRandomMental_AndQuestionExists_ReturnAnswerIsNotCorrect() {
            var questionFileAccessorMock = CreateQuestionFileAccessorMock();
            questionFileAccessorMock.Setup(x => x.Find(It.IsAny<Predicate<Question>>()))
                .Returns(new Question() {
                    QuestionId = "test",
                    Exercise = "test exercise",
                    Answer = "correct answer"
                });

            var exerciseService = new ExerciseService(questionFileAccessorMock.Object);

            var testAnswer = new Question() {
                QuestionId = "test",
                Exercise = "test exercise",
                Answer = "wrong answer",
                Category = CategoryType.ANALYSIS
            };

            var result = exerciseService.CheckAnswer(testAnswer);

            Assert.False(result);
        }

        [Fact]
        public void CheckAnswer_WhenGivenAnswer_AndQuestionIsRandomMental_ReturnAnswerIsCorrect() {
            var questionFileAccessorMock = CreateQuestionFileAccessorMock();
            questionFileAccessorMock.Setup(x => x.Find(It.IsAny<Predicate<Question>>()))
                .Returns((Question) null);

            var exerciseService = new ExerciseService(questionFileAccessorMock.Object);

            var testAnswer = new Question() {
                QuestionId = "test",
                Exercise = "1 + 1",
                Answer = "2",
                Category = CategoryType.RANDOMMENTAL
            };

            var result = exerciseService.CheckAnswer(testAnswer);

            Assert.True(result);
        }

        [Fact]
        public void CheckAnswer_WhenGivenAnswer_AndQuestionIsRandomMental_ReturnAnswerIsNotCorrect() {
            var questionFileAccessorMock = CreateQuestionFileAccessorMock();
            questionFileAccessorMock.Setup(x => x.Find(It.IsAny<Predicate<Question>>()))
                .Returns((Question) null);

            var exerciseService = new ExerciseService(questionFileAccessorMock.Object);

            var testAnswer = new Question() {
                QuestionId = "test",
                Exercise = "1 + 1",
                Answer = "4",
                Category = CategoryType.ANALYSIS
            };

            var result = exerciseService.CheckAnswer(testAnswer);

            Assert.False(result);
        }
        #endregion
    }
}
