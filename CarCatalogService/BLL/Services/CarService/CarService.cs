using AutoMapper;
using CarCatalogService.BLL.Services.CarService.Models;
using CarCatalogService.DAL.Entities;
using CarCatalogService.DAL.Repositories.Interfaces;

namespace CarCatalogService.BLL.Services.CarService;

/// <summary>
///     Provides functionality for managing car-related operations.
/// </summary>
public class CarService : ICarService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Car> _carRepository;

    /// <summary>
    ///     Initializes a new instance of the <see cref="CarService"/> class.
    /// </summary>
    /// <param name="mapper">An <see cref="IMapper"/> instance for object mapping.</param>
    /// <param name="carRepository">An <see cref="IRepository{T}"/> for accessing car entities.</param>
    public CarService(IMapper mapper, IRepository<Car> carRepository)
    {
        _mapper = mapper;
        _carRepository = carRepository;
    }

    /// <inheritdoc/>
    public async Task AddCarAsync(AddCarModel model)
    {
        var car = _mapper.Map<Car>(model);
        await _carRepository.CreateAsync(car);
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteCarAsync(long carId)
    {
        return await _carRepository.DeleteAsync(carId);
    }

    /// <inheritdoc/>
    public async Task<CarModel?> GetCarAsync(long carId)
    {
        var car = await _carRepository.GetAsync(carId);

        var data = _mapper.Map<CarModel>(car);
        return data;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<CarModel>> GetAllCarsAsync()
    {
        var data = (await _carRepository.GetAllAsync())
            .Select(_mapper.Map<CarModel>);

        return data;
    }

    /// <inheritdoc/>
    public async Task<bool> UpdateCarAsync(long carId, UpdateCarModel model)
    {
        var car = await _carRepository.GetAsync(carId);

        if (car == null)
            return false;

        car = _mapper.Map(model, car);

        await _carRepository.UpdateAsync(car);
        return true;
    }
}
