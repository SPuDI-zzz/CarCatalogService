using AutoMapper;
using CarCatalogService.Services.CarService.Models;

namespace CarCatalogService.ViewModels;

public class EditCarViewModel
{
    public long Id { get; set; }
    public required string Mark { get; set; }
    public required string Model { get; set; }
    public required string Color { get; set; }
    public long UserId { get; set; }
}

public class EditCarViewModelProfile : Profile
{
    public EditCarViewModelProfile()
    {
        CreateMap<CarModel, EditCarViewModel>();
        CreateMap<EditCarViewModel, UpdateCarModel>();
    }
}
