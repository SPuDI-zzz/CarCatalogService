namespace CarCatalogService.BLL.Services.UserService.Models;

/// <summary>
///     Represents a model that encapsulates information about a user.
/// </summary>
public class UserModel
{
    /// <summary>
    ///     Gets or sets the unique identifier for the user.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    ///     Gets or sets the login name for the user.
    /// </summary>
    /// <remarks>This property is required.</remarks>
    public required string Login { get; set; }

    /// <summary>
    ///     Gets or sets the roles assigned for the user.
    /// </summary>
    /// <remarks>This property is required.</remarks>
    public required IEnumerable<string> Roles { get; set; }
}
