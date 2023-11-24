using System.Security.Claims;

namespace CarCatalogService.Shared.Extensions;

/// <summary>
///     Provides extension methods for working with claims associated with a <see cref="ClaimsPrincipal"/> instance.
/// </summary>
public static class ClaimsPrincipalExtensions
{
    /// <summary>
    ///     Gets the user's unique identifier from the claims.
    /// </summary>
    /// <param name="user">The <see cref="ClaimsPrincipal"/> representing the user.</param>
    /// <returns>
    ///     The unique identifier of the user if found; otherwise, the default value for <see cref="long"/>.
    /// </returns>
    public static long GetUserId(this ClaimsPrincipal user)
    {
        var nameIdentifier = user.FindFirstValue(ClaimTypes.NameIdentifier);
        _ = long.TryParse(nameIdentifier, out var userId);
        return userId;
    }
}
