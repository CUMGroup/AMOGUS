using AMOGUS.Core.Centralization.User;
using AMOGUS.Core.Common.Interfaces.User;
using AMOGUS.Core.DataTransferObjects.User;
using Microsoft.AspNetCore.Mvc;

namespace AMOGUS.Api.Controllers {
    [ApiController]
    [Route("auth")]
    public class AuthController : Controller {

        private readonly IAuthService _authService;

        public AuthController(IAuthService _authService) {
            this._authService = _authService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginApiModel model) {
            var res = await _authService.LoginUserAsync(model);
            return res.Match<IActionResult>(
                Ok,
                Unauthorized
            );
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterApiModel model) {
            var res = await _authService.RegisterUserAsync(model, UserRoles.User);
            return res.Match<IActionResult>(
                Ok,
                UnprocessableEntity
            );
        }
    }
}
