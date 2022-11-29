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
        public async Task<IActionResult> Login([FromBody]LoginApiModel model) {
            LoginResultApiModel res = await _authService.LoginUserAsync(model);
            return res.Result.Succeeded ? Ok(res) : Unauthorized(res);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]RegisterApiModel model) {
            LoginResultApiModel res = await _authService.RegisterUserAsync(model, UserRoles.User);
            return res.Result.Succeeded ? Ok(res) : UnprocessableEntity(res);
        }
    }
}
