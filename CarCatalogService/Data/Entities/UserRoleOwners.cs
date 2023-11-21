using Microsoft.AspNetCore.Identity;

namespace CarCatalogService.Data.Entities;

public class UserRoleOwners : IdentityUserRole<long>
{
    public User User { get; set; }
    public UserRole Role { get; set; }
}
