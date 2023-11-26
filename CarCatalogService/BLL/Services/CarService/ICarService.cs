using CarCatalogService.BLL.Services.CarService.Models;

namespace CarCatalogService.BLL.Services.CarService;

/// <summary>
///     Defines the contract for a service responsible for managing car-related operations.
/// </summary>
public interface ICarService
{
    /// <summary>
    ///     Asynchronously retrieves a specific car by its unique identifier.
    /// </summary>
    /// <param name="carId">The unique identifier of the car.</param>
    /// <returns>
    ///     A <see cref="Task"/> representing the asynchronous operation.
    ///     The task result is a <see cref="CarModel"/> representing the details of the requested car.
    ///     If the car is not found, the result is <c>null</c>.
    /// </returns>
    Task<CarModel?> GetCarAsync(long carId);

    /// <summary>
    ///     Asynchronously retrieves all cars available in the system.
    /// </summary>
    /// <returns>
    ///     A <see cref="Task"/> representing the asynchronous operation.
    ///     The task result is an enumerable of <see cref="CarModel"/> 
    ///     representing all available cars.
    /// </returns>
    Task<IEnumerable<CarModel>> GetAllCarsAsync();

    /// <summary>
    ///     Asynchronously adds a new car to the system based on the provided information.
    /// </summary>
    /// <param name="model">
    ///     A <see cref="AddCarModel"/> containing the details of the new car to be added.
    /// </param>
    /// <returns>
    ///     A <see cref="Task"/> representing the asynchronous operation.
    /// </returns>
    Task AddCarAsync(AddCarModel model);

    /// <summary>
    ///     Asynchronously updates the details of an existing car based on the provided information.
    /// </summary>
    /// <param name="carId">The unique identifier of the car to be updated.</param>
    /// <param name="model">A <see cref="UpdateCarModel"/> containing the updated details for the car.</param>
    /// <returns>
    ///     A <see cref="Task"/> representing the asynchronous operation.
    ///     The result is a <see cref="bool"/> indicating whether the edeting was successful (true) or not (false).
    /// </returns>
    Task<bool> UpdateCarAsync(long carId, UpdateCarModel model);

    /// <summary>
    ///     Asynchronously deletes a car from the system based on its unique identifier.
    /// </summary>
    /// <param name="carId">The unique identifier of the car to be deleted.</param>
    /// <returns>
    ///     A <see cref="Task"/> representing the asynchronous operation.
    ///     The result is a <see cref="bool"/> indicating whether the deletion was successful (true) or not (false).
    /// </returns>
    Task<bool> DeleteCarAsync(long carId);
}
