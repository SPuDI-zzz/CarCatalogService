using AutoMapper;
using CarCatalogService.BLL.Services.CarService;
using CarCatalogService.BLL.Services.CarService.Models;
using CarCatalogService.Shared.Const;
using CarCatalogService.Shared.Extensions;
using CarCatalogService.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarCatalogService.Controllers;

/// <summary>
///     Controller responsible for handling actions related to cars, such as listing, creation, editing, and deletion.
/// </summary>
public class CarsController : Controller
{
    private readonly ICarService _carService;
    private readonly IMapper _mapper;

    /// <summary>
    ///     Initializes a new instance of the <see cref="CarsController"/> class.
    /// </summary>
    /// <param name="carService">The service responsible for car operations.</param>
    /// <param name="mapper">The AutoMapper instance for mapping between models.</param>
    public CarsController(ICarService carService, IMapper mapper)
    {
        _carService = carService;
        _mapper = mapper;
    }

    /// <summary>
    ///     Displays a list of all cars.
    /// </summary>
    /// <returns>The view displaying a list of cars.</returns>
    /// <remarks>
    ///     This action requires that users authorized.
    /// </remarks>
    [Authorize(Policy = AppRoles.User)]
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var response = await _carService.GetAllCarsAsync();
        return View(response);
    }

    /// <summary>
    ///     Displays the view for creating a new car.
    /// </summary>
    /// <returns>The view for creating a new car.</returns>
    /// <remarks>
    ///     This action requires users to have the <see cref="AppRoles.Manager"/>
    ///     or <see cref="AppRoles.Admin"/> role for access.
    /// </remarks>
    [Authorize(Policy = AppRoles.Manager)]
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    /// <summary>
    ///     Handles the POST request for creating a new car.
    /// </summary>
    /// <param name="carViewModel">The view model containing information for creating a new car.</param>
    /// <returns>
    ///     Redirects to the car list view on successful creation; otherwise,
    ///     returns the create view with an error message.
    /// </returns>
    /// <remarks>
    ///     This action requires users to have the <see cref="AppRoles.Manager"/>
    ///     or <see cref="AppRoles.Admin"/> role for access.
    /// </remarks>
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

    /// <summary>
    ///     Displays the view for editing a car.
    /// </summary>
    /// <param name="id">The unique identifier of the car to edit</param>
    /// <returns>
    ///     If the car is found, returns the edit confirmation view with details of the car;
    ///     otherwise, returns an error view.
    /// </returns>
    /// <remarks>
    ///     This action requires users to have the <see cref="AppRoles.Manager"/>
    ///     or <see cref="AppRoles.Admin"/> role for access.
    /// </remarks>
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

    /// <summary>
    ///     Handles the HTTP POST request for editing an existing car.
    /// </summary>
    /// <param name="id">The unique identifier of the car to edit.</param>
    /// <param name="carViewModel">The view model containing updated information for the car.</param>
    /// <returns>
    ///     Redirects to the car list view on successful update; otherwise, returns the edit view with an error message.
    /// </returns>
    /// <remarks>
    ///     This action requires users to have the <see cref="AppRoles.Manager"/>
    ///     or <see cref="AppRoles.Admin"/> role for access.
    /// </remarks>
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

    /// <summary>
    ///     Displays the view for deleting a car.
    /// </summary>
    /// <param name="id">The unique identifier of the car to delete</param>
    /// <returns>
    ///     If the car is found, returns the delete confirmation view with details of the car;
    ///     otherwise, returns an error view.
    /// </returns>
    /// <remarks>
    ///     This action requires users to have the <see cref="AppRoles.Manager"/>
    ///     or <see cref="AppRoles.Admin"/> role for access.
    /// </remarks>
    [Authorize(Policy = AppRoles.Manager)]
    [HttpGet]
    public async Task<IActionResult> Delete(long id)
    {
        var carModel = await _carService.GetCarAsync(id);
        if (carModel == null)
            return View("Error");

        return View(carModel);
    }

    /// <summary>
    ///     Handles the HTTP POST request for deleting an existing car.
    /// </summary>
    /// <param name="id">The unique identifier of the car to delete.</param>
    /// <returns>
    ///     Redirects to the car list view on successful delete; otherwise, returns an error view.
    /// </returns>
    /// <remarks>
    ///     This action requires users to have the <see cref="AppRoles.Manager"/>
    ///     or <see cref="AppRoles.Admin"/> role for access.
    /// </remarks>
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
