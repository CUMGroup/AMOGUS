using AMOGUS.Core.Common.Exceptions;
using AMOGUS.Core.Common.Interfaces.Game;
using AMOGUS.Core.Common.Interfaces.Security;
using AMOGUS.Core.Domain.Models.Entities;
using AMOGUS.Core.Services.Teacher;

namespace AMOGUS.UnitTests.Tests.Core {
    public class TeacherServiceTests {
        private Mock<IQuestionFileAccessor> CreateIQuestionFileAccessorMock() => new();
        private Mock<ITokenFactory> CreateTokenFactoryMock() => new();

        #region AddQuestionAsync
        [Fact]
        public async Task AddQuestionAsync_WhenGivenQuestion_AndEverythingIsFine_AddsQuestionAndReturnsQuestion() {
            var questionsList = new List<Question>();
            var testGuid = Guid.NewGuid();

            var tokenFactoryMock = CreateTokenFactoryMock();
            tokenFactoryMock
                .Setup(x => x.GenerateGuidToken())
                .Returns(testGuid);

            var questionFileAccessorMock = CreateIQuestionFileAccessorMock();
            questionFileAccessorMock
                .Setup(x => x.GetAllQuestions())
                .Returns(questionsList);

            var testQuestion = new Question();

            var teacherService = new TeacherService(questionFileAccessorMock.Object, tokenFactoryMock.Object);
            var result = await teacherService.AddQuestionAsync(testQuestion);

            Assert.True(result.IsSuccess);
            Assert.True(result.Value.QuestionId.Equals(testGuid.ToString()));
            Assert.True(questionsList.Count == 1);
        }

        [Fact]
        public async Task AddQuestionAsync_WhenGivenQuestion_AndIOException_ReturnsException() {
            var tokenFactoryMock = CreateTokenFactoryMock();
            tokenFactoryMock
                .Setup(x => x.GenerateGuidToken())
                .Returns(Guid.NewGuid());

            var questionFileAccessorMock = CreateIQuestionFileAccessorMock();
            questionFileAccessorMock
                .Setup(x => x.GetAllQuestions())
                .Returns(new List<Question>());

            questionFileAccessorMock
                .Setup(x => x.SaveQuestionFilesAsync())
                .Returns(() => throw new IOException());

            var testQuestion = new Question();

            var teacherService = new TeacherService(questionFileAccessorMock.Object, tokenFactoryMock.Object);
            var result = await teacherService.AddQuestionAsync(testQuestion);

            Assert.True(result.IsFaulted);
            Assert.True(result.exception is IOException);
        }
        #endregion

        #region DeleteQuestionByIdAsync
        [Fact]
        public async Task DeleteQuestionByIdAsync_WhenGivenQuestionId_AndQuestionDoesNotExist_ReturnsException() {
            var questionFileAccessorMock = CreateIQuestionFileAccessorMock();
            questionFileAccessorMock
                .Setup(x => x.GetAllQuestions())
                .Returns(new List<Question>());

            var teacherService = new TeacherService(questionFileAccessorMock.Object, CreateTokenFactoryMock().Object);
            var result = await teacherService.DeleteQuestionByIdAsync("testId");

            Assert.True(result.IsFaulted);
            Assert.True(result.exception is RecordNotFoundException);
        }

        [Fact]
        public async Task DeleteQuestionByIdAsync_WhenGivenQuestionId_AndQuestionFileNotAccessible_ReturnsException() {
            var questionFileAccessorMock = CreateIQuestionFileAccessorMock();
            questionFileAccessorMock
                .Setup(x => x.GetAllQuestions())
                .Returns(new List<Question>() {
                    new Question() { QuestionId = "testId"}
                });

            questionFileAccessorMock
                .Setup(x => x.SaveQuestionFilesAsync())
                .Returns(() => throw new IOException());

            var teacherService = new TeacherService(questionFileAccessorMock.Object, CreateTokenFactoryMock().Object);
            var result = await teacherService.DeleteQuestionByIdAsync("testId");

            Assert.True(result.IsFaulted);
            Assert.True(result.exception is IOException);
        }

        [Fact]
        public async Task DeleteQuestionByIdAsync_WhenGivenQuestionId_AndEverythingIsFine_ReturnsTrue_AndQuestionDeleted() {
            var questionsList = new List<Question>() {
                new Question() {QuestionId = "testId"}
            };

            var questionFileAccessorMock = CreateIQuestionFileAccessorMock();
            questionFileAccessorMock
                .Setup(x => x.GetAllQuestions())
                .Returns(questionsList);

            var teacherService = new TeacherService(questionFileAccessorMock.Object, CreateTokenFactoryMock().Object);
            var result = await teacherService.DeleteQuestionByIdAsync("testId");

            Assert.True(result.IsSuccess);
            Assert.True(result == true);
            Assert.True(questionsList.Count == 0);
        }
        #endregion

        #region GetQuestionById
        [Fact]
        public void GetQuestionById_WhenGivenQuestionId_AndQuestionDoesNotExist_ReturnsException() {
            var questionFileAccessorMock = CreateIQuestionFileAccessorMock();
            questionFileAccessorMock
                .Setup(x => x.GetAllQuestions())
                .Returns(new List<Question>());

            var teacherService = new TeacherService(questionFileAccessorMock.Object, CreateTokenFactoryMock().Object);
            var result = teacherService.GetQuestionById("testId");

            Assert.True(result.IsFaulted);
            Assert.True(result.exception is RecordNotFoundException);
        }
        [Fact]
        public void GetQuestionById_WhenGivenQuestionId_AndQuestionDoesExist_ReturnsQuestion() {
            var questionFileAccessorMock = CreateIQuestionFileAccessorMock();
            questionFileAccessorMock
                .Setup(x => x.GetAllQuestions())
                .Returns(new List<Question>() {
                    new Question(){ QuestionId = "testId" }
                });

            var teacherService = new TeacherService(questionFileAccessorMock.Object, CreateTokenFactoryMock().Object);
            var result = teacherService.GetQuestionById("testId");

            Assert.True(result.IsSuccess);
            Assert.True(result.Value.QuestionId.Equals("testId"));
        }
        #endregion
    }
}
