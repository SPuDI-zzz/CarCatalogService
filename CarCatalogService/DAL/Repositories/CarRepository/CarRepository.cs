using CarCatalogService.DAL.Entities;
using CarCatalogService.DAL.EntityFramework;
using CarCatalogService.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Xml.Schema;

namespace CarCatalogService.DAL.Repositories.CarRepository;

public class CarRepository : IRepository<Car>
{
    private readonly IDbContextFactory<MainDbContext> _dbContextFactory;

    public CarRepository(IDbContextFactory<MainDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task CreateAsync(Car item)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        await context.Cars.AddAsync(item);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var car = await context.Cars.FirstOrDefaultAsync(car => car.Id.Equals(id))
            ?? throw new Exception($"The car (id: {id}) was not found");

        context.Remove(car);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Car>> GetAllAsync()
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var cars = await context.Cars.ToListAsync();
        return cars;
    }

    public async Task<Car?> GetAsync(long id)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var car = await context.Cars.FirstOrDefaultAsync(car => car.Id.Equals(id));
        return car;
    }

    public async Task UpdateAsync(Car item)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        context.Cars.Update(item);
        await context.SaveChangesAsync();
    }
}
