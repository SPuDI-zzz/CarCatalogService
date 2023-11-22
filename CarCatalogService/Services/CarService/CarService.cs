using AutoMapper;
using CarCatalogService.Data;
using CarCatalogService.Data.Entities;
using CarCatalogService.Data.EntityFramework;
using CarCatalogService.Data.Repositories.Interfaces;
using CarCatalogService.Services.CarService.Models;
using Microsoft.EntityFrameworkCore;

namespace CarCatalogService.Services.CarService;

public class CarService : ICarService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Car> _carRepository;

    public CarService(IMapper mapper, IRepository<Car> carRepository)
    {
        _mapper = mapper;
        _carRepository = carRepository;
    }

    public async Task AddCar(AddCarModel model)
    {
        var car = _mapper.Map<Car>(model);
        await _carRepository.CreateAsync(car);
    }

    public async Task DeleteCar(long carId)
    {
        await _carRepository.DeleteAsync(carId);
    }

    public async Task<CarModel> GetCar(long carId)
    {
        var car = await _carRepository.GetAsync(carId);

        var data = _mapper.Map<CarModel>(car);
        return data;
    }

    public async Task<IEnumerable<CarModel>> GetAllCars()
    {
        var data = (await _carRepository.GetAllAsync())
            .Select(_mapper.Map<CarModel>);

        return data;
    }

    public async Task UpdateCar(long carId, UpdateCarModel model)
    {
        var car = await _carRepository.GetAsync(carId)
            ?? throw new Exception($"The car (id: {carId}) was not found");

        car = _mapper.Map(model, car);

        await _carRepository.UpdateAsync(car);
    }
}
