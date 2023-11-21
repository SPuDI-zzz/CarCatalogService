using AutoMapper;
using CarCatalogService.Services.UserService.Models;
using CarCatalogService.Shared;

namespace CarCatalogService.ViewModels;

public class AddUserViewModel
{
    public required string Login { get; set; }
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
