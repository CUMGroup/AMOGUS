
using AMOGUS.Core.Common.Communication;
using AMOGUS.Core.Domain.Models.Entities;

namespace AMOGUS.Core.Common.Interfaces.Teacher {
    public interface ITeacherService {

        List<Question> GetAllQuestions();
        Result<Question> GetQuestionById(string questionId);

        Task<Result> DeleteQuestionByIdAsync(string questionId);

        Task<Result<Question>> AddQuestionAsync(Question question);

    }
}
