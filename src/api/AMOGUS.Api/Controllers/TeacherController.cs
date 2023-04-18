using AMOGUS.Core.Centralization.User;
using AMOGUS.Core.Common.Exceptions;
using AMOGUS.Core.Common.Interfaces.Teacher;
using AMOGUS.Core.Domain.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMOGUS.Api.Controllers {
    [Route("teachers")]
    [Authorize(Roles = $"{UserRoles.Moderator},{UserRoles.Admin}")]
    [ApiController]
    public class TeacherController : ControllerBase {

        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService) {
            _teacherService = teacherService!;
        }

        [HttpGet]
        [Route("questions")]
        public IActionResult GetAllQuestions() {
            var res = _teacherService.GetAllQuestions();
            return Ok(res);
        }

        [HttpGet]
        [Route("questions/{id}")]
        public IActionResult GetQuestionById([FromRoute] string id) {
            var res = _teacherService.GetQuestionById(id);
            return res.Match<IActionResult>(
                Ok,
                err => {
                    if (err is RecordNotFoundException) {
                        return NotFound();
                    }
                    return StatusCode(500);
                }
            );
        }

        [HttpPost]
        [Route("questions")]
        public async Task<IActionResult> AddQuestion([FromBody] Question quest) {
            var res = await _teacherService.AddQuestionAsync(quest);
            return res.Match<IActionResult>(
                Ok,
                err => StatusCode(500)
            );
        }

        [HttpDelete]
        [Route("questions/{id}")]
        public async Task<IActionResult> DeleteQuestion([FromRoute] string id) {
            var res = await _teacherService.DeleteQuestionByIdAsync(id);
            return res.Match<IActionResult>(
                Ok,
                err => {
                    if (err is RecordNotFoundException)
                        return NotFound();
                    return StatusCode(500);
                }
            );
        }

    }
}
