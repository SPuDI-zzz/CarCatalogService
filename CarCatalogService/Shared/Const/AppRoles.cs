namespace CarCatalogService.Shared.Const;

/// <summary>
///     Static class containing constants representing roles within the application.
/// </summary>
public static class AppRoles
{
    /// <summary>
    ///     Represents a regular user role with the ability to look at car data.
    /// </summary>
    public const string User = "User";

    /// <summary>
    ///     Represents a manager role with the ability to manage car data.
    /// </summary>
    public const string Manager = "Manager";

    /// <summary>
    ///     Represents an administrator role with full system access.
    /// </summary>
    public const string Admin = "Admin";
}
