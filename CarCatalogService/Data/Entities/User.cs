using CarCatalogService.Data.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace CarCatalogService.Data.Entities
{
    public class User : BaseEntity
    {
        [MinLength(4)]
        public required string Login { get; set; }
        [MinLength(8)]
        public required string Password { get; set; }
        public long RoleId { get; set; }
        public required Role Role { get; set; }
        public required ICollection<Car> Cars { get; set; }
    }
}
