using CarCatalogService.BLL.Services.AccountService.Models;

namespace CarCatalogService.BLL.Services.AccountService;

/// <summary>
///     Defines the contract for a service responsible for user account management operations.
/// </summary>
public interface IAccountService
{
    /// <summary>
    ///     Asynchronously registers a new user account based on the provided information.
    /// </summary>
    /// <param name="model">
    ///     A <see cref="RegisterUserAccountModel"/> containing the user's registration details.
    /// </param>
    /// <returns>
    ///     A <see cref="Task"/> representing the asynchronous operation.
    /// </returns>
    Task RegisterAsync(RegisterUserAccountModel model);

    /// <summary>
    ///     Asynchronously performs user authentication based on the provided login information.
    /// </summary>
    /// <param name="model">A <see cref="LoginUserAccountModel"/> containing the user's login details.</param>
    /// <returns>
    ///     A <see cref="Task"/> representing the asynchronous operation.
    ///     The task result is a string containing an authentication token if login is successful.
    /// </returns>
    Task<string> LoginAsync(LoginUserAccountModel model);
}
