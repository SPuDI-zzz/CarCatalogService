using AutoMapper;
using CarCatalogService.Data.Entities;

namespace CarCatalogService.Services.CarSercice.Models;

public class UpdateCarModel
{
    public long Id { get; set; }
    public required string Mark { get; set; }
    public required string Model { get; set; }
    public required string Color { get; set; }
    public long UserId { get; set; }
}

public class UpdateCarModelProfile : Profile
{
    public UpdateCarModelProfile()
    {
        CreateMap<Car, UpdateCarModel>();
    }
}
