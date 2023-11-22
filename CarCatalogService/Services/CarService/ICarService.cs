using CarCatalogService.Services.CarService.Models;

namespace CarCatalogService.Services.CarService;

public interface ICarService
{
    Task<CarModel> GetCar(long carId);
    Task<IEnumerable<CarModel>> GetAllCars();
    Task AddCar(AddCarModel model);
    Task UpdateCar(long carId, UpdateCarModel model);
    Task DeleteCar(long carId);
}
