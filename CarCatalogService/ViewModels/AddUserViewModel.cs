using AutoMapper;
using CarCatalogService.Services.UserService.Models;
using CarCatalogService.Shared;
using System.ComponentModel.DataAnnotations;

namespace CarCatalogService.ViewModels;

public class AddUserViewModel
{
    [Required(ErrorMessage = "Login is required")]
    public required string Login { get; set; }
    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public required string Password {  get; set; }
    public required IEnumerable<RolesEnum> Roles { get; set; }
}

public class AddUserViewModelProfile : Profile
{
    public AddUserViewModelProfile()
    {
        CreateMap<AddUserViewModel, AddUserModel>()
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles.Select(val => val.ToString())));
    }
}
