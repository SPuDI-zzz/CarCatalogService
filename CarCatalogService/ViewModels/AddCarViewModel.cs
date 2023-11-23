using AutoMapper;
using CarCatalogService.BLL.Services.CarService.Models;
using System.ComponentModel.DataAnnotations;

namespace CarCatalogService.ViewModels;

public class AddCarViewModel
{
    [Required(ErrorMessage = "Mark is required")]
    public string Mark { get; set; } = default!;
    [Required(ErrorMessage = "Model is required")]
    public required string Model { get; set; } = default!;
    [Required(ErrorMessage = "Color is required")]
    public required string Color { get; set; } = default!;
    public long UserId { get; set; }
}

public class AddCarViewModelProfile : Profile
{
    public AddCarViewModelProfile()
    {
        CreateMap<AddCarViewModel, AddCarModel>();
    }
}
