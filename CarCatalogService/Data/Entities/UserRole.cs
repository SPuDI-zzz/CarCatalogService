using CarCatalogService.Data.Entities.Common;
using Microsoft.AspNetCore.Identity;

namespace CarCatalogService.Data.Entities;

public class UserRole : IdentityRole<long>
{
    public virtual ICollection<UserRoleOwners> UserRoles { get; set; } = default!;
}
