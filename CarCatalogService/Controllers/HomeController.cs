using CarCatalogService.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace CarCatalogService.Controllers;

public class HomeController : Controller
{
    [AllowAnonymous]
    [Authorize(Policy = "AllowAnonymousPolicy")]
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
