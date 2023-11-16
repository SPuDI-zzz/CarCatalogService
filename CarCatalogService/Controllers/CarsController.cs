using CarCatalogService.Services.CarService;
using CarCatalogService.Services.CarService.Models;
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
        var response = await _carService.GetAllCars();
        return View(response);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(AddCarModel carModel)
    {
        if (!ModelState.IsValid)
        {
            return View(carModel);
        }
        await _carService.AddCar(carModel);
        return RedirectToAction(nameof(Index));
    }
}
