using AMOGUS.Core.Common.Exceptions;
using AMOGUS.Core.Common.Interfaces.Game;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AMOGUS.Api.Controllers {
    [ApiController]
    [Authorize]
    [Route("stats")]
    public class StatsController : Controller {

        private readonly IStatsService _statsService;

        public StatsController(IStatsService statsService) {
            _statsService = statsService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetStats() {
            var userId = GetUserId();

            if (userId == null) {
                return Forbid();
            }
            var res = await _statsService.GetDetailedUserStatsModel(userId);
            return res.Match<IActionResult>(
                Ok,
                err => {
                    if (err is RecordNotFoundException)
                        return NotFound();
                    return StatusCode(500, err.Message);
                }
            );
        }

        private string? GetUserId() {
            return HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
