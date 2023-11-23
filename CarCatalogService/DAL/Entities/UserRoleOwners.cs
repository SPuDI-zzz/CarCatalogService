using Microsoft.AspNetCore.Identity;

namespace CarCatalogService.DAL.Entities;

public class UserRoleOwners : IdentityUserRole<long>
{
    public required User User { get; set; }
    public required UserRole Role { get; set; }
}
