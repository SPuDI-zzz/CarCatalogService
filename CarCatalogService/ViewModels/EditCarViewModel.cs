﻿using AutoMapper;
using CarCatalogService.BLL.Services.CarService.Models;
using CarCatalogService.ViewModels.Common;
using System.ComponentModel.DataAnnotations;

namespace CarCatalogService.ViewModels;

/// <summary>
///     Represents a view model for editing an existing car entity.
/// </summary>
public class EditCarViewModel : BaseViewModel
{
    /// <summary>
    ///     Gets or sets the identifier of the car entity to edit.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    ///     Gets or sets the mark of the car.
    /// </summary>
    [Required(ErrorMessage = "Mark is required")]
    public string Mark { get; set; } = default!;

    /// <summary>
    ///     Gets or sets the model of the car.
    /// </summary>
    [Required(ErrorMessage = "Model is required")]
    public string Model { get; set; } = default!;

    /// <summary>
    ///     Gets or sets the color of the car.
    /// </summary>
    [Required(ErrorMessage = "Color is required")]
    public string Color { get; set; } = default!;

    /// <summary>
    ///     Gets or sets the identifier of the user associated with the car.
    /// </summary>
    public long UserId { get; set; }
}

/// <summary>
///     AutoMapper profile for mapping <see cref="CarModel"/> entities to <see cref="EditCarViewModel"/>
///     and <see cref="EditCarViewModel"/> entities to <see cref="UpdateCarModel"/> classes.
/// </summary>
public class EditCarViewModelProfile : Profile
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="EditCarViewModelProfile"/> class.
    /// </summary>
    public EditCarViewModelProfile()
    {
        CreateMap<CarModel, EditCarViewModel>();
        CreateMap<EditCarViewModel, UpdateCarModel>();
    }
}
