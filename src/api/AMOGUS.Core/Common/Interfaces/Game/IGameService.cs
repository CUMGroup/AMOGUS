
using AMOGUS.Core.Domain.Models.Game;
using System.Xml;

namespace AMOGUS.Core.Common.Interfaces.Game {
    public interface IGameService {

        Task<Session> NewSessionAsync();

        Task<Session> EndSessionAsync(Session session);
    }
}
