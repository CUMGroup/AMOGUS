
using AMOGUS.Core.Domain.Models.Entities;
using System.Xml;

namespace AMOGUS.Core.Common.Interfaces.Game {
    public interface IGameService {

        Task<GameSession> NewSessionAsync();

        Task<GameSession> EndSessionAsync(GameSession session);
    }
}
