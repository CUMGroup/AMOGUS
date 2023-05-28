using AMOGUS.Core.Common.Interfaces.Game;
using AMOGUS.Core.Domain.Enums;
using AMOGUS.Core.Domain.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

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
        public IActionResult NewSession([FromBody] CategoryType category) {
            var userId = GetUserId();
            if (userId is null) {
                return Forbid();
            }
            var options = new JsonSerializerOptions { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            string session = JsonSerializer.Serialize(_gameService.NewSession(category, userId), options);
            return Ok(Base64Encode(session));
        }

        [Route("end")]
        [HttpPost]
        public async Task<IActionResult> EndSession([FromBody] GameSession gameSession) {
            var userId = GetUserId();
            if (userId is null) {
                return Forbid();
            }
            await _gameService.EndSessionAsync(gameSession, userId);
            return Ok();
        }

        private string? GetUserId() {
            return HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        private string Base64Encode(string plainText) {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}
