using AMOGUS.Core.Common.Interfaces.Game;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMOGUS.Api.Controllers {

    [ApiController]
    [Route("information")]
    public class InformationController : Controller {

        private readonly ILeaderboardService _leaderboardService;

        public InformationController(ILeaderboardService leaderboardService) {
            _leaderboardService = leaderboardService;
        }

        [HttpGet]
        [Authorize]
        [Route("leaderboards")]
        public async Task<ActionResult> GetLeaderboards() {
            var boards = await _leaderboardService.GetLeaderboardAsync();
            return Ok(boards);
        }
    }
}
