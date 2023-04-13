using AMOGUS.Core.Domain.Enums;
using AMOGUS.Core.Domain.Models.Entities;

namespace AMOGUS.Core.Common.Interfaces.Game {
    public interface IGameService {

        GameSession NewSession(CategoryType category, string userId);

        Task EndSessionAsync(GameSession session, string userId);
    }
}
