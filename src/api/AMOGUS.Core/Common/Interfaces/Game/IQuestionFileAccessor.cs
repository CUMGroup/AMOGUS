using AMOGUS.Core.Domain.Enums;
using AMOGUS.Core.Domain.Models.Entities;

namespace AMOGUS.Core.Common.Interfaces.Game {
    public interface IQuestionFileAccessor {

        void ReloadQuestions();

        List<Question> GetAllQuestions();

        List<Question> GetAllQuestionsByCategory(CategoryType category);

        List<Question> GetRandomQuestionsByCategory(CategoryType category, int amount);

        Question? Find(Predicate<Question> predicate);

        Task SaveQuestionFilesAsync();

        CategoryType[] GetAllCategories();

    }
}
