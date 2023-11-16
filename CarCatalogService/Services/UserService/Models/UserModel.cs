using AutoMapper;
using CarCatalogService.Data.Entities;

namespace CarCatalogService.Services.UserService.Models;

public class UserModel
{
    public long Id { get; set; }
    public required string Login { get; set; }
    public required string Password { get; set; }
    public long RoleId { get; set; }
    public required string RoleName { get; set; }
}

public class UserModelProfile : Profile
{
    public UserModelProfile()
    {
        CreateMap<User, UserModel>()
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name));
    }
}
