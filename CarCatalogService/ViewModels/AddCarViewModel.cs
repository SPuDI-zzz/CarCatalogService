using AutoMapper;
using CarCatalogService.BLL.Services.CarService.Models;
using CarCatalogService.ViewModels.Common;
using System.ComponentModel.DataAnnotations;

namespace CarCatalogService.ViewModels;

/// <summary>
///     Represents a view model for creating a new car entity.
/// </summary>
public class AddCarViewModel : BaseViewModel
{
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
