using Microsoft.AspNetCore.Mvc;

namespace CarCatalogService.Controllers
{
    [Route("Errors")]
    public class ErrorsController : Controller
    {
        [HttpGet("{statusCode}")]
        public IActionResult Index([FromRoute]string statusCode)
        {
            return View((object)statusCode);
        }
    }
}
