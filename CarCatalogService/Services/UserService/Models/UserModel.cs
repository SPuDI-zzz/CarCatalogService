using AutoMapper;
using CarCatalogService.Data.Entities;

namespace CarCatalogService.Services.UserService.Models;

public class UserModel
{
    public long Id { get; set; }
    public required string Login { get; set; }
    public required string Role { get; set; }
}

public class UserModelProfile : Profile
{
    public UserModelProfile()
    {
        CreateMap<User, UserModel>()
            .ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.UserName));
        CreateMap<UserRole, UserModel>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Name));
    }
}
