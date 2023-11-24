using Microsoft.AspNetCore.Identity;

namespace CarCatalogService.DAL.Entities;

/// <summary>
///     Represents a user entity in the application data model, inheriting from <see cref="IdentityUser{TKey}"/>.
/// </summary>
public class User : IdentityUser<long>
{
    /// <summary>
    ///     Gets or sets the collection of user roles associated with the user.
    /// </summary>
    /// <remarks>This property is virtual.</remarks>
    public virtual ICollection<UserRoleOwners> UserRoles { get; set; } = default!;
    /// <summary>
    ///     Gets or sets the collection of cars associated with the user.
    /// </summary>
    /// <remarks>This property is virtual.</remarks>
    public virtual ICollection<Car> Cars { get; set; } = default!;
}
