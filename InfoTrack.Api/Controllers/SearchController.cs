using Microsoft.AspNetCore.Mvc;

namespace InfoTrack.Api.Controllers
{
    [ApiController]
    [Route("search")]
    public class SearchController : ControllerBase
    {
        [HttpGet]
        [Route("all")]
        public ActionResult<string> GetAll()
        {
            return Ok("Tests");
        }
    }
}
