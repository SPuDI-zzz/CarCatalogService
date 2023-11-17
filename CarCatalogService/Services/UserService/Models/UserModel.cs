using AutoMapper;
using CarCatalogService.Data.Entities;

namespace CarCatalogService.Services.UserService.Models;

public class UserModel
{
    public long Id { get; set; }
    public required string Login { get; set; }
    public required string Password { get; set; }
}

public class UserModelProfile : Profile
{
    public UserModelProfile()
    {
        CreateMap<User, UserModel>();
    }
}
