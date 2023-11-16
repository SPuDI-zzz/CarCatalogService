using AutoMapper;
using CarCatalogService.Data.Entities;

namespace CarCatalogService.Services.CarService.Models;

public class CarModel
{
    public long Id { get; set; }
    public required string Mark { get; set; }
    public required string Model { get; set; }
    public required string Color { get; set; }
    public long UserId { get; set; }
}

public class CarModelProfile : Profile
{
    public CarModelProfile()
    {
        CreateMap<Car, CarModel>();
    }
}
