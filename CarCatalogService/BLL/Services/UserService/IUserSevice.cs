using CarCatalogService.BLL.Services.UserService.Models;

namespace CarCatalogService.BLL.Services.UserService;

/// <summary>
///     Defines the contract for a service responsible for managing user-related operations.
/// </summary>
public interface IUserSevice
{
    /// <summary>
    ///     Asynchronously retrieves a specific user by their unique identifier.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <returns>
    ///     A <see cref="Task"/> representing the asynchronous operation.
    ///     The task result is a <see cref="UserModel"/> representing the details of the requested user.
    ///     If the user is not found, the result is <c>null</c>.
    /// </returns>
    Task<UserModel?> GetUserAsync(long userId);

    /// <summary>
    ///     Asynchronously retrieves all users available in the system.
    /// </summary>
    /// <returns>
    ///     A <see cref="Task"/> representing the asynchronous operation.
    ///     The task result is an enumerable of <see cref="UserModel"/> representing all available users.
    /// </returns>
    Task<IEnumerable<UserModel>> GetAllUsersAsync();

    /// <summary>
    ///     Asynchronously adds a new user to the system based on the provided information.
    /// </summary>
    /// <param name="model">An <see cref="AddUserModel"/> containing the details of the new user to be added.</param>
    /// <returns>
    ///     A <see cref="Task"/> representing the asynchronous operation.
    ///     The result is an <see cref="AddUserAccountResponseModel"/> indicating the status of the user account addition.
    /// </returns>
    Task<AddUserAccountResponseModel> AddUserAsync(AddUserModel model);

    /// <summary>
    ///     Asynchronously updates the details of an existing user based on the provided information.
    /// </summary>
    /// <param name="userId">The unique identifier of the user to be updated.</param>
    /// <param name="model">An <see cref="UpdateUserModel"/> containing the updated details for the user.</param>
    /// <returns>
    ///     A <see cref="Task"/> representing the asynchronous operation.
    ///     The result is an <see cref="UpdateUserAccountResponseModel"/> indicating the status of the user account update.
    /// </returns>
    Task<UpdateUserAccountResponseModel> UpdateUserAsync(long userId, UpdateUserModel model);

    /// <summary>
    ///     Asynchronously deletes a user from the system based on their unique identifier.
    /// </summary>
    /// <param name="userId">The unique identifier of the user to be deleted.</param>
    /// <returns>
    ///     A <see cref="Task"/> representing the asynchronous operation.
    ///     The result is a <see cref="bool"/> indicating whether the deletion was successful (true) or not (false).
    /// </returns>
    Task<bool> DeleteUserAsync(long userId);
}
