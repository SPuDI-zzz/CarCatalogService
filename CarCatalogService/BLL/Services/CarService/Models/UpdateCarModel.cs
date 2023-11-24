using AutoMapper;
using CarCatalogService.DAL.Entities;

namespace CarCatalogService.BLL.Services.CarService.Models;

/// <summary>
///     Represents a model for updating an existing car.
/// </summary>
public class UpdateCarModel
{
    /// <summary>
    ///     Gets or sets the mark name for the updated car.
    /// </summary>
    /// <remarks>This property is required.</remarks>
    public required string Mark { get; set; }

    /// <summary>
    ///     Gets or sets the model name for the updated car.
    /// </summary>
    /// <remarks>This property is required.</remarks>
    public required string Model { get; set; }

    /// <summary>
    ///     Gets or sets the color for the updated car.
    /// </summary>
    /// <remarks>This property is required.</remarks>
    public required string Color { get; set; }

    /// <summary>
    ///     Gets or sets the unique identifier for the user associated with the updated car.
    /// </summary>
    public long UserId { get; set; }
}

/// <summary>
///     AutoMapper profile for mapping <see cref="UpdateCarModel"/> entities to <see cref="Car"/> objects.
/// </summary>
public class UpdateCarModelProfile : Profile
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="UpdateCarModelProfile"/> class.
    /// </summary>
    public UpdateCarModelProfile()
    {
        CreateMap<UpdateCarModel, Car>();
    }
}
