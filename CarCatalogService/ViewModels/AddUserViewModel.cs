using AutoMapper;
using CarCatalogService.Services.UserService.Models;
using CarCatalogService.Shared;

namespace CarCatalogService.ViewModels;

public class AddUserViewModel
{
    public required string Login { get; set; }
    public required string Password {  get; set; }
    public required RolesEnum Role { get; set; }
}

public class AddUserViewModelProfile : Profile
{
    public AddUserViewModelProfile()
    {
        CreateMap<AddUserViewModel, AddUserModel>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));
    }
}
