namespace CarCatalogService.BLL.Services.AccountService.Models;

/// <summary>
///     Represents the response model for user account login operations.
/// </summary>
public class LoginUserAccountResponseModel
{
    /// <summary>
    ///     Gets or sets a value indicating whether the login operation resulted in an error.
    /// </summary>
    public bool IsError { get; set; }

    /// <summary>
    ///     Gets or sets the JWT token generated upon successful login.
    /// </summary>
    public string Token { get; set; } = default!;
}
