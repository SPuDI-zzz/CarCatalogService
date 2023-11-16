using AutoMapper;
using CarCatalogService.Data.Entities;

namespace CarCatalogService.Services.UserService.Models;

public class UpdateUserModel
{
    public required string Login { get; set; }
    public required string Password { get; set; }
    public long RoleId { get; set; }
}

public class UpdateUserModelProfile : Profile
{
    public UpdateUserModelProfile()
    {
        CreateMap<UpdateUserModel, User>();
    }
}
