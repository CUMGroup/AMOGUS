using AMOGUS.Core.Common.Exceptions;
using AMOGUS.Core.Common.Interfaces.Game;
using AMOGUS.Core.Common.Interfaces.Security;
using AMOGUS.Core.Domain.Models.Entities;
using AMOGUS.Core.Services.Gameplay;
using AMOGUS.Core.Services.Teacher;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMOGUS.UnitTests {
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
        // test if RecordNotFoundException when quest null
        // test if IO exception is catched
        // test if all other exception is catched
        // test if deleted
        #endregion

        #region GetQuestionById
        // test if RecordNotFoundException when quest null
        // test if question returned
        #endregion
    }
}
