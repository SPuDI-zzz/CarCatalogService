namespace CarCatalogService.BLL.Services.UserService.Models;

/// <summary>
///     Represents the response model for updating a user account.
/// </summary>
public class UpdateUserAccountResponseModel
{
    /// <summary>
    ///     Gets or sets a value indicating whether the user account update operation resulted in an error.
    /// </summary>
    public bool IsError { get; set; }

    /// <summary>
    ///     Gets or sets an error message providing details about the user account update failure, if applicable.
    /// </summary>
    public string ErrorMessage { get; set; } = default!;
}
