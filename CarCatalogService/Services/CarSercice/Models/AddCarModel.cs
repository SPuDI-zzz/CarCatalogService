using AutoMapper;
using CarCatalogService.Data.Entities;

namespace CarCatalogService.Services.CarSercice.Models;

public class AddCarModel
{
    public required string Mark { get; set; }
    public required string Model { get; set; }
    public required string Color { get; set; }
    public long UserId { get; set; }
}

public class AddCarModelProfile : Profile
{
    public AddCarModelProfile()
    {
        CreateMap<Car, AddCarModel>();
    }
}
