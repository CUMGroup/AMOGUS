using Microsoft.AspNetCore.Mvc;

namespace AMOGUS.Api.Controllers {

    [ApiController]
    [Route("health")]
    public class HealthController : Controller {


        [HttpGet]
        [Route("")]
        public IActionResult HealthCheck() {
            return Ok("Everything is healthy!");
        }

    }
}
