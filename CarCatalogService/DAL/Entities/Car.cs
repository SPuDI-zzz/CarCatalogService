using CarCatalogService.DAL.Entities.Common;

namespace CarCatalogService.DAL.Entities;

public class Car : BaseEntity
{
    public required string Mark { get; set; }
    public required string Model { get; set; }
    public required string Color { get; set; }
    public long UserId { get; set; }
    public virtual User? User { get; set; }
}
