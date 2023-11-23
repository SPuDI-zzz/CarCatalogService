using AutoMapper;
using CarCatalogService.BLL.Services.CarService;
using CarCatalogService.BLL.Services.CarService.Models;
using CarCatalogService.DAL.Entities;
using CarCatalogService.Shared.Const;
using CarCatalogService.Shared.Extensions;
using CarCatalogService.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

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
        var response = await _carService.GetAllCarsAsync();
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
            return View(carViewModel);

        carViewModel.UserId = User.GetUserId();

        var carModel = _mapper.Map<AddCarModel>(carViewModel);

        await _carService.AddCarAsync(carModel);
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Policy = AppRoles.Manager)]
    [HttpGet]
    public async Task<IActionResult> Edit(long id)
    {
        var car = await _carService.GetCarAsync(id);
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

        await _carService.UpdateCarAsync(id, carModel);

        return RedirectToAction(nameof(Index));
    }

    [Authorize(Policy = AppRoles.Manager)]
    [HttpGet]
    public async Task<IActionResult> Delete(long id)
    {
        var carModel = await _carService.GetCarAsync(id);
        if (carModel == null)
            return View("Error");

        return View(carModel);
    }

    [Authorize(Policy = AppRoles.Manager)]
    [HttpPost, ActionName(nameof(Delete))]
    public async Task<IActionResult> DeleteCar(long id)
    {
        var carModel = await _carService.GetCarAsync(id);
        if (carModel == null)
            return View("Error");

        await _carService.DeleteCarAsync(id);

        return RedirectToAction(nameof(Index));
    }
}
