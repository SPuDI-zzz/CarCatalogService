using CarCatalogService.Data.Entities.Common;

namespace CarCatalogService.Data.Entities;

public class Car : BaseEntity
{
    public required string Mark { get; set; }
    public required string Model { get; set; }
    public required string Color { get; set; }
    public long UserId { get; set; }
    public virtual required User User { get; set; }
}
