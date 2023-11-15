using AutoMapper;
using CarCatalogService.Data;
using CarCatalogService.Data.Entities;
using CarCatalogService.Services.CarSercice.Models;
using Microsoft.EntityFrameworkCore;

namespace CarCatalogService.Services.CarSercice;

public class CarService : ICarService
{
    private readonly IDbContextFactory<MainDbContext> _contextFactory;
    private readonly IMapper _mapper;

    public CarService(IDbContextFactory<MainDbContext> contextFactory, IMapper mapper)
    {
        _contextFactory = contextFactory;
        _mapper = mapper;
    }

    public async Task<CarModel> AddCar(AddCarModel model)
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        var car = _mapper.Map<Car>(model);

        await context.Cars.AddAsync(car);
        await context.SaveChangesAsync();

        var data = _mapper.Map<CarModel>(car);
        return data;
    }

    public async Task DeleteCar(long carId)
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        var car = await context.Cars.FirstOrDefaultAsync(id => id.Equals(carId))
            ?? throw new Exception($"The car (id: {carId}) was not found");

        context.Remove(car);
        context.SaveChanges();
    }

    public async Task<CarModel> GetCar(long carId)
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        var car = await context.Cars.FirstOrDefaultAsync(id => id.Equals(carId));

        var data = _mapper.Map<CarModel>(car);
        return data;
    }

    public async Task<IEnumerable<CarModel>> GetCars()
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        var data = (await context.Cars.ToListAsync())
            .Select(_mapper.Map<CarModel>);

        return data;
    }

    public async Task<CarModel> UpdateCar(long carId, UpdateCarModel model)
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        var car = await context.Cars.FirstOrDefaultAsync(id => id.Equals(carId))
            ?? throw new Exception($"The car (id: {carId}) was not found");

        car = _mapper.Map(model, car);

        context.Cars.Update(car);
        context.SaveChanges();

        var data = _mapper.Map<CarModel>(car);
        return data;
    }
}
