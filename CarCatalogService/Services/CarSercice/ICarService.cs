using CarCatalogService.Services.CarSercice.Models;

namespace CarCatalogService.Services.CarSercice;

public interface ICarService
{
    Task<CarModel> GetCar(long carId);
    Task<IEnumerable<CarModel>> GetCars();
    Task<CarModel> AddCar(AddCarModel model);
    Task<CarModel> UpdateCar(long carId, UpdateCarModel model);
    Task DeleteCar(long carId);
}
