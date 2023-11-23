using CarCatalogService.DAL.Entities.Common;
using Microsoft.AspNetCore.Identity;

namespace CarCatalogService.DAL.Entities;

public class UserRole : IdentityRole<long>
{
    public virtual ICollection<UserRoleOwners> UserRoles { get; set; } = default!;
}
