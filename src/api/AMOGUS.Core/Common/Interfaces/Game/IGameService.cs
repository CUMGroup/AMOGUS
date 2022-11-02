
using AMOGUS.Core.Domain.Models.Game;
using System.Xml;

namespace AMOGUS.Core.Common.Interfaces.Game {
    public interface IGameService {

        Task<Session> NewSession();

        Task<Session> EndSession(Session session);
    }
}
