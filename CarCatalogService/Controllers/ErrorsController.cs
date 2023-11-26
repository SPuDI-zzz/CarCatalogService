using CarCatalogService.Shared.Const;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarCatalogService.Controllers
{
    /// <summary>
    ///     Controller responsible for handling error pages with specific HTTP status codes.
    /// </summary>
    [Route("Errors")]
    [AllowAnonymous]
    [Authorize(Policy = AppRoles.User)]
    public class ErrorsController : Controller
    {
        /// <summary>
        ///     Displays the error page corresponding to the given HTTP status code.
        /// </summary>
        /// <param name="statusCode">The HTTP status code indicating the type of error.</param>
        /// <returns>
        ///     The view representing the result of the error page display with the provided HTTP status code.
        /// </returns>
        [HttpGet("{statusCode}")]
        public IActionResult Index([FromRoute]string statusCode)
        {
            return View((object)statusCode);
        }
    }
}
