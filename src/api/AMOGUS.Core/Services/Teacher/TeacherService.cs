using AMOGUS.Core.Common.Communication;
using AMOGUS.Core.Common.Exceptions;
using AMOGUS.Core.Common.Interfaces.Game;
using AMOGUS.Core.Common.Interfaces.Security;
using AMOGUS.Core.Common.Interfaces.Teacher;
using AMOGUS.Core.Domain.Models.Entities;

namespace AMOGUS.Core.Services.Teacher {
    internal class TeacherService : ITeacherService {

        private readonly IQuestionFileAccessor _questionFileAccessor;
        private readonly ITokenFactory _tokenFactory;

        public TeacherService(IQuestionFileAccessor questionFileAccessor, ITokenFactory tokenFactory) {
            _questionFileAccessor = questionFileAccessor!;
            _tokenFactory = tokenFactory!;
        }

        public async Task<Result<Question>> AddQuestionAsync(Question question) {
            question.QuestionId = _tokenFactory.GenerateGuidToken().ToString();
            try {
                _questionFileAccessor.GetAllQuestions().Add(question);
                await _questionFileAccessor.SaveQuestionFilesAsync();
            }
            catch (IOException ex) {
                return ex;
            }
            catch (Exception ex) {
                return ex;
            }
            return question;
        }

        public async Task<Result> DeleteQuestionByIdAsync(string questionId) {
            var quest = _questionFileAccessor.GetAllQuestions().FirstOrDefault(e => e.QuestionId.Equals(questionId));
            if (quest == null) {
                return new RecordNotFoundException($"Could not find question with id {questionId}");
            }
            try {
                _questionFileAccessor.GetAllQuestions().Remove(quest);
                await _questionFileAccessor.SaveQuestionFilesAsync();
            }
            catch (IOException ex) {
                return ex;
            }
            catch (Exception ex) {
                return ex;
            }
            return true;
        }

        public List<Question> GetAllQuestions() {
            return _questionFileAccessor.GetAllQuestions();
        }

        public Result<Question> GetQuestionById(string questionId) {
            var quest = _questionFileAccessor.GetAllQuestions().FirstOrDefault(e => e.QuestionId.Equals(questionId));
            if (quest == null) {
                return new RecordNotFoundException($"Could not find question with id {questionId}");
            }
            return quest;
        }
    }
}
