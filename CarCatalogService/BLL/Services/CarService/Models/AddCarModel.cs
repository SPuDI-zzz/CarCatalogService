using AutoMapper;
using CarCatalogService.DAL.Entities;

namespace CarCatalogService.BLL.Services.CarService.Models;

/// <summary>
///     Represents a model for adding a new car.
/// </summary>
public class AddCarModel
{
    /// <summary>
    ///     Gets or sets the mark name of the new car.
    /// </summary>
    /// <remarks>This property is required.</remarks>
    public required string Mark { get; set; }

    /// <summary>
    ///     Gets or sets the model name of the new car.
    /// </summary>
    /// <remarks>This property is required.</remarks>
    public required string Model { get; set; }

    /// <summary>
    ///     Gets or sets the color of the new car.
    /// </summary>
    /// <remarks>This property is required.</remarks>
    public required string Color { get; set; }

    /// <summary>
    ///     Gets or sets the unique identifier for the user associated with the new car.
    /// </summary>
    /// <remarks>This property is required.</remarks>
    public long UserId { get; set; }
}

/// <summary>
///     AutoMapper profile for mapping <see cref="AddCarModel"/> entities to <see cref="Car"/> objects.
/// </summary>
public class AddCarModelProfile : Profile
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="AddCarModelProfile"/> class.
    /// </summary>
    public AddCarModelProfile()
    {
        CreateMap<AddCarModel, Car>();
    }
}
