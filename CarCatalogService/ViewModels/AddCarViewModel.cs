using AutoMapper;
using CarCatalogService.BLL.Services.CarService.Models;
using System.ComponentModel.DataAnnotations;

namespace CarCatalogService.ViewModels;

/// <summary>
///     Represents a view model for creating a new car entity.
/// </summary>
public class AddCarViewModel
{
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
///     AutoMapper profile for mapping <see cref="AddCarViewModel"/> entities to <see cref="AddCarModel"/> objects.
/// </summary>
public class AddCarViewModelProfile : Profile
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="AddCarViewModelProfile"/> class.
    /// </summary>
    public AddCarViewModelProfile()
    {
        CreateMap<AddCarViewModel, AddCarModel>();
    }
}
