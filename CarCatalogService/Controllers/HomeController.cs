using CarCatalogService.Shared.Const;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarCatalogService.Controllers;

/// <summary>
///     Controller responsible for handling home-related actions.
/// </summary>
public class HomeController : Controller
{
    /// <summary>
    ///     Displays the home page.
    /// </summary>
    /// <returns>The home page view.</returns>
    /// <remarks>
    ///     It is decorated with the <see cref="AllowAnonymousAttribute"/> to allow access to both authenticated
    ///     and unauthenticated users. Additionally, it is protected by the <see cref="AuthorizeAttribute"/> with
    ///     the role specified in <see cref="AppRoles.User"/> to restrict access only to users with the specified role.
    /// </remarks>
    [AllowAnonymous]
    [Authorize(Policy = AppRoles.User)]
    public IActionResult Index()
    {
        return View();
    }

    /// <summary>
    /// Displays the privacy page.
    /// </summary>
    /// <returns>The privacy page view.</returns>
    [AllowAnonymous]
    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult ErrorPage(string statusCode)
    {
        return View(statusCode);
    }
}
