using AutoMapper;
using CarCatalogService.BLL.Services.UserService.Models;
using CarCatalogService.Shared.Enum;

namespace CarCatalogService.ViewModels;

public class EditUserViewModel
{
    public long Id { get; set; }
    public required string Login { get; set; }
    public required IEnumerable<RolesEnum> Roles { get; set; }
}

public class EditUserViewModelProfile : Profile
{
    public EditUserViewModelProfile()
    {
        CreateMap<UserModel, EditUserViewModel>()
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles.Select(role => (RolesEnum)Enum.Parse(typeof(RolesEnum), role))));
        CreateMap<EditUserViewModel, UpdateUserModel>()
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles.Select(role => role.ToString())));
    }
}
