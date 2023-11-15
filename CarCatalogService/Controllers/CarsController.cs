using CarCatalogService.Services.CarSercice;
using Microsoft.AspNetCore.Mvc;

namespace CarCatalogService.Controllers;

public class CarsController : Controller
{
    private readonly ICarService _carService;
    public CarsController(ICarService carService)
    {
        _carService = carService;
    }

    public async Task<IActionResult> Index()
    {
        var cars = await _carService.GetCars();
        return View(cars);
    }
}
