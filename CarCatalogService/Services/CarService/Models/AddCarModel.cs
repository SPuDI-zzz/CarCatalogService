using AutoMapper;
using CarCatalogService.Data.Entities;

namespace CarCatalogService.Services.CarService.Models;

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
        CreateMap<AddCarModel, Car>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId == 0 ? 1 : src.UserId));
    }
}
