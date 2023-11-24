using CarCatalogService.DAL.Entities.Common;

namespace CarCatalogService.DAL.Entities;

/// <summary>
///     Represents a car entity in the application data model, inheriting from <see cref="BaseEntity"/>.
/// </summary>
public class Car : BaseEntity
{
    /// <summary>
    ///     Gets or sets the mark of the car.
    /// </summary>
    /// <remarks>This property is required.</remarks>
    public required string Mark { get; set; }

    /// <summary>
    ///     Gets or sets the model of the car.
    /// </summary>
    /// <remarks>This property is required.</remarks>
    public required string Model { get; set; }

    /// <summary>
    ///     Gets or sets the color of the car.
    /// </summary>
    /// <remarks>This property is required.</remarks>
    public required string Color { get; set; }

    /// <summary>
    ///     Gets or sets the identifier of the associated user.
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    ///     Gets or sets the navigation property to the associated user.
    /// </summary>
    /// <remarks>This property is virtual.</remarks>
    public virtual User? User { get; set; }
}
