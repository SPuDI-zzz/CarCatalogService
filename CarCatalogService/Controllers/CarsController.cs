using AutoMapper;
using CarCatalogService.Services.CarService;
using CarCatalogService.Services.CarService.Models;
using CarCatalogService.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CarCatalogService.Controllers;

public class CarsController : Controller
{
    private readonly ICarService _carService;
    private readonly IMapper _mapper;
    public CarsController(ICarService carService, IMapper mapper)
    {
        _carService = carService;
        _mapper = mapper;
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

    public async Task<IActionResult> Edit(long id)
    {
        var car = await _carService.GetCar(id);
        if (car == null)
            return View("Error");

        var carViewModel = _mapper.Map<EditCarViewModel>(car);
        return View(carViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(long id, EditCarViewModel carViewModel)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Failed to edit car");
            return View(nameof(Edit), carViewModel);
        }

        var carModel = _mapper.Map<UpdateCarModel>(carViewModel);

        await _carService.UpdateCar(id, carModel);

        return RedirectToAction(nameof(Index));
    }
}
