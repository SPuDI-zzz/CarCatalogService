using AutoMapper;
using CarCatalogService.Services.CarService;
using CarCatalogService.Services.CarService.Models;
using CarCatalogService.Shared;
using CarCatalogService.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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

    [Authorize(Policy = AppRoles.User)]
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var response = await _carService.GetAllCars();
        return View(response);
    }

    [Authorize(Policy = AppRoles.Manager)]
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [Authorize(Policy = AppRoles.Manager)]
    [HttpPost]
    public async Task<IActionResult> Create(AddCarViewModel carViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(carViewModel);
        }

        var carModel = _mapper.Map<AddCarModel>(carViewModel);

        await _carService.AddCar(carModel);
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Policy = AppRoles.Manager)]
    [HttpGet]
    public async Task<IActionResult> Edit(long id)
    {
        var car = await _carService.GetCar(id);
        if (car == null)
            return View("Error");

        var carViewModel = _mapper.Map<EditCarViewModel>(car);
        return View(carViewModel);
    }

    [Authorize(Policy = AppRoles.Manager)]
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

    [Authorize(Policy = AppRoles.Manager)]
    [HttpGet]
    public async Task<IActionResult> Delete(long id)
    {
        var carModel = await _carService.GetCar(id);
        if (carModel == null)
            return View("Error");

        return View(carModel);
    }

    [Authorize(Policy = AppRoles.Manager)]
    [HttpPost, ActionName(nameof(Delete))]
    public async Task<IActionResult> DeleteCar(long id)
    {
        var carModel = await _carService.GetCar(id);
        if (carModel == null)
            return View("Error");

        await _carService.DeleteCar(id);

        return RedirectToAction(nameof(Index));
    }
}
