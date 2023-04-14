
using AMOGUS.Core.Domain.Models.Entities;

namespace AMOGUS.Core.Common.Interfaces.Repositories {
    public interface IUserMedalRepository {

        Task<int> DeleteUserMedalsByUserIdAsync(string userId);

        Task<List<UserMedal>> GetUserMedalsByUserIdAsync(string userId);

    }
}
