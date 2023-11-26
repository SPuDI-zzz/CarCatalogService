namespace CarCatalogService.BLL.Services.AccountService.Models;

/// <summary>
///     Represents the response model for user account registration.
/// </summary>
public class RegisterUserAccountResponseModel
{
    /// <summary>
    ///     Gets or sets a value indicating whether the registration operation encountered an error.
    /// </summary>
    public bool IsError { get; set; }

    /// <summary>
    ///     Gets or sets the error message associated with the registration operation, if any.
    /// </summary>
    /// <remarks>
    ///     This property is populated with details about the encountered errors when <see cref="IsError"/> is true.
    /// </remarks>
    public string ErrorMessage { get; set; } = default!;
}
