using AutoMapper;
using CarCatalogService.BLL.Services.CarService.Models;
using System.ComponentModel.DataAnnotations;

namespace CarCatalogService.ViewModels;

public class EditCarViewModel
{
    public long Id { get; set; }
    [Required(ErrorMessage = "Mark is required")]
    public required string Mark { get; set; }
    [Required(ErrorMessage = "Model is required")]
    public required string Model { get; set; }
    [Required(ErrorMessage = "Color is required")]
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
