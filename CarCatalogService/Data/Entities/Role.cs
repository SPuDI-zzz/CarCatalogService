using CarCatalogService.Data.Entities.Common;

namespace CarCatalogService.Data.Entities;

public class Role : BaseEntity
{
    public required string Name { get; set; }
    public virtual required ICollection<User> Users { get; set; }
}
