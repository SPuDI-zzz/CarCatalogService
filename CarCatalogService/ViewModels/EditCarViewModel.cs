using AutoMapper;
using CarCatalogService.BLL.Services.CarService.Models;
using System.ComponentModel.DataAnnotations;

namespace CarCatalogService.ViewModels;

/// <summary>
///     Represents a view model for editing an existing car entity.
/// </summary>
public class EditCarViewModel
{
    /// <summary>
    ///     Gets or sets the identifier of the car entity to edit.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    ///     Gets or sets the mark of the car.
    /// </summary>
    /// <remarks>This property is required.</remarks>
    [Required(ErrorMessage = "Mark is required")]
    public required string Mark { get; set; }

    /// <summary>
    ///     Gets or sets the model of the car.
    /// </summary>
    /// <remarks>This property is required.</remarks>
    [Required(ErrorMessage = "Model is required")]
    public required string Model { get; set; }

    /// <summary>
    ///     Gets or sets the color of the car.
    /// </summary>
    /// <remarks>This property is required.</remarks>
    [Required(ErrorMessage = "Color is required")]
    public required string Color { get; set; }

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
