using Microsoft.AspNetCore.Identity;

namespace CarCatalogService.DAL.Entities;

/// <summary>
///     Represents a user role entity in the application data model, inheriting from <see cref="IdentityRole{TKey}"/>.
/// </summary>
public class UserRole : IdentityRole<long>
{
    /// <summary>
    ///     Gets or sets the collection of user roles associated with the user.
    /// </summary>
    /// <remarks>This property is virtual.</remarks>
    public virtual ICollection<UserRoleOwners> UserRoles { get; set; } = default!;
}
