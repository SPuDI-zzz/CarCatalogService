using AutoMapper;
using CarCatalogService.Services.UserService.Models;
using CarCatalogService.Shared;

namespace CarCatalogService.ViewModels;

public class EditUserViewModel
{
    public long Id { get; set; }
    public required string Login { get; set; }
    public RolesEnum Role { get; set; }
}

public class EditUserViewModelProfile : Profile
{
    public EditUserViewModelProfile()
    {
        CreateMap<UserModel, EditUserViewModel>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => (RolesEnum)Enum.Parse(typeof(RolesEnum), src.Role)));
        CreateMap<EditUserViewModel, UpdateUserModel>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));
    }
}
