using CarCatalogService.Data.Entities.Common;

namespace CarCatalogService.Data.Entities
{
    public class User : BaseEntity
    {
        public required string Login { get; set; }
        public required string Password { get; set; }
        public required Role Role { get; set; }
    }
}
