using CarCatalogService.Services.CarService.Models;

namespace CarCatalogService.Services.CarService;

public interface ICarService
{
    Task<CarModel> GetCar(long carId);
    Task<IEnumerable<CarModel>> GetAllCars();
    Task<CarModel> AddCar(AddCarModel model);
    Task<CarModel> UpdateCar(long carId, UpdateCarModel model);
    Task DeleteCar(long carId);
}
