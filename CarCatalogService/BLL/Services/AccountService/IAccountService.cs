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
    ///     A <see cref="Task"/> representing the asynchronous operation. A response model indicating the success or failure of the registration operation.
    /// </returns>
    Task<RegisterUserAccountResponseModel> RegisterAsync(RegisterUserAccountModel model);

    /// <summary>
    ///     Asynchronously authenticates a user based on the provided login credentials and generates a JWT token upon successful authentication.
    /// </summary>
    /// <param name="model">A <see cref="LoginUserAccountModel"/> containing the user's login details.</param>
    /// <returns>
    ///     A <see cref="Task"/> representing the asynchronous operation.
    ///     The result is a <see cref="LoginUserAccountResponseModel"/> 
    ///     containing information about the authentication status and the JWT token if authentication is successful.
    /// </returns>
    Task<LoginUserAccountResponseModel> LoginAsync(LoginUserAccountModel model);
}
