using AMOGUS.Core.Common.Communication;
using AMOGUS.Core.Common.Exceptions;
using AMOGUS.Core.Common.Interfaces.User;
using AMOGUS.Core.DataTransferObjects.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AMOGUS.Api.Controllers {
    [ApiController]
    [Route("user")]
    public class UserController : Controller {
        private readonly IUserService _userService;

        public UserController(IUserService userService) {
            _userService = userService;
        }

        [HttpGet]
        [Route("profile")]
        [Authorize]
        public async Task<IActionResult> GetUser() {
            var userId = HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) {
                return Forbid();
            }
            var userResult = await _userService.GetUserAsync(userId);

            return userResult.Match<IActionResult>(
                succ => Ok(UserApiModel.MapFromUserModel(succ)),
                err => {
                    if (err is UserNotFoundException)
                        return NotFound(err.Message);
                    return StatusCode(500, err.Message);
                }
            );
        }

        [HttpDelete]
        [Route("profile")]
        [Authorize]
        public async Task<IActionResult> DeleteUser() {
            var userId = HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) {
                return Forbid();
            }
            Result res = await _userService.DeleteUserAsync(userId);
            return res.Match<IActionResult>(
                Ok,
                err => {
                    if(err is UserNotFoundException)
                        return NotFound(err.Message);
                    if(err is UserOperationException)
                        return UnprocessableEntity(err.Message);
                    return StatusCode(500, err.Message);
                }
            );
        }
    }
}
