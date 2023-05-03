using AMOGUS.Core.Common.Interfaces.Abstractions;
using AMOGUS.Core.Common.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMOGUS.UnitTests {
    public class StatsServiceTests {
        private Mock<IUserStatsRepository> CreateUserStatsRepositoryMock() => new();
        private Mock<IGameSessionRepository> CreateGameSessionRepositoryMock() => new();
        private Mock<IDateTime> CreateDateTimeMock() => new();

        #region GetUserStatsAsync
        // UserOperationException
        // Everythiing alright
        #endregion

        #region GetDetailedUserStatsModelAsync
        // RecordNotFoundException
        // Everythiing alright
        #endregion

        #region UpdateUserStatsAsync(UserStats userStats)
        // return false
        // res = 0 (fehler)
        // res > 0
        #endregion

        #region UpdateUserStatsAsync(GameSession session, bool[] answers, ApplicationUser user)
        // return false
        // res = 0
        // res > 0
        // stats are updated
        // if not played so far, does streak increase?
        #endregion
    }
}
