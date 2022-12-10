using AMOGUS.Core.Common.Communication;
using AMOGUS.Core.Common.Exceptions;
using AMOGUS.Core.Common.Interfaces.User;
using AMOGUS.Core.DataTransferObjects.User;
using AMOGUS.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AMOGUS.Api.Controllers {
    [ApiController]
    [Route("user")]
    public class UserController : Controller{
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
            try{
                ApplicationUser user = await _userService.GetUserAsync(userId);
                UserApiModel userModel = new(user.Id, user.UserName, user.Email, user.PlayedToday);

                return Ok(userModel);
            }
            catch (UserNotFoundException ex) {
                return NotFound();
            }
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
            return res.Succeeded ? Ok(res) : NotFound();
        }
    }
}
