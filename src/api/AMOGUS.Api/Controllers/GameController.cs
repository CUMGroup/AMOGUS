using AMOGUS.Core.Common.Interfaces.Game;
using AMOGUS.Core.Domain.Enums;
using AMOGUS.Core.Domain.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AMOGUS.Api.Controllers {

    [Route("game")]
    [Authorize]
    public class GameController : Controller {

        private readonly IGameService _gameService;

        public GameController(IGameService gameService) {
            _gameService = gameService;
        }

        [Route("new")]
        [HttpPost]
        public IActionResult NewSession([FromBody]CategoryType category) {
            var userId = GetUserId();
            if(userId is null) {
                return Forbid();
            }
            return Ok(_gameService.NewSession(category, userId));
        }

        [Route("end")]
        [HttpPost]
        public async Task<IActionResult> EndSession([FromBody]GameSession gameSession) {
            var userId = GetUserId();
            if(userId is null) {
                return Forbid();
            }
            await _gameService.EndSessionAsync(gameSession, userId);
            return Ok();
        }

        private string? GetUserId() {
            return HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
