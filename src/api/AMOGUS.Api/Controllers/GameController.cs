using Microsoft.AspNetCore.Mvc;
using System.Web.Http;

namespace AMOGUS.Api.Controllers {

    [Route("game")]
    public class GameController : ApiController {
        public IActionResult Index() {
            return View();
        }
    }
}
