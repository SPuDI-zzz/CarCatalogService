using AutoMapper;
using CarCatalogService.Services.CarService.Models;

namespace CarCatalogService.ViewModels;

public class AddCarViewModel
{
    public required string Mark { get; set; }
    public required string Model { get; set; }
    public required string Color { get; set; }
    public long UserId { get; set; }
}

public class AddCarViewModelProfile : Profile
{
    public AddCarViewModelProfile()
    {
        CreateMap<AddCarViewModel, AddCarModel>();
    }
}
