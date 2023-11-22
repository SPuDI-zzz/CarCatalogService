using CarCatalogService.Data.Entities.Common;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CarCatalogService.Data.Entities
{
    public class User : IdentityUser<long>
    {
        public virtual ICollection<UserRoleOwners> UserRoles { get; set; } = default!;
        public virtual ICollection<Car> Cars { get; set; } = default!;
    }
}
