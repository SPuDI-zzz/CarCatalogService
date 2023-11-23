using AutoMapper;
using CarCatalogService.DAL.Entities;

namespace CarCatalogService.BLL.Services.CarService.Models;

/// <summary>
///     Represents a model that encapsulates information about a car.
/// </summary>
public class CarModel
{
    /// <summary>
    ///     Gets or sets the unique identifier for the car.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    ///     Gets or sets the mark name of the car.
    /// </summary>
    /// <remarks>This property is required.</remarks>
    public required string Mark { get; set; }

    /// <summary>
    ///     Gets or sets the model name of the car.
    /// </summary>
    /// <remarks>This property is required.</remarks>
    public required string Model { get; set; }

    /// <summary>
    ///     Gets or sets the color of the car.
    /// </summary>
    /// <remarks>This property is required.</remarks>
    public required string Color { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the user associated with the car.
    /// </summary>
    /// <remarks>This property is required.</remarks>
    public long UserId { get; set; }
}

/// <summary>
///     AutoMapper profile for mapping <see cref="Car"/> entities to <see cref="CarModel"/> objects.
/// </summary>
public class CarModelProfile : Profile
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="CarModelProfile"/> class.
    /// </summary>
    public CarModelProfile()
    {
        CreateMap<Car, CarModel>();
    }
}
