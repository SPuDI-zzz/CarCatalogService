using AutoMapper;
using CarCatalogService.Data.Entities;

namespace CarCatalogService.Services.UserService.Models;

public class UserModel
{
    public long Id { get; set; }
    public required string Login { get; set; }
    public required IEnumerable<string> Roles { get; set; }
}

public class UserModelProfile : Profile
{
    public UserModelProfile()
    {
        CreateMap<UserRoleOwners, UserModel>()
            .ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Role.Name))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId));
    }
}
