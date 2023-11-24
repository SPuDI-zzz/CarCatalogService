using Microsoft.AspNetCore.Identity;

namespace CarCatalogService.DAL.Entities;

/// <summary>
///     Represents the association between a user and a role in the application data model,
///     inheriting from <see cref="IdentityUserRole{TKey}"/>.
/// </summary>
public class UserRoleOwners : IdentityUserRole<long>
{
    /// <summary>
    ///     Gets or sets the user associated with the role.
    /// </summary>
    /// <remarks>This property is virtual and required.</remarks>
    public virtual required User User { get; set; }
    /// <summary>
    ///     Gets or sets the role associated with the user.
    /// </summary>
    /// <remarks>This property is virtual and required.</remarks>
    public virtual required UserRole Role { get; set; }
}
