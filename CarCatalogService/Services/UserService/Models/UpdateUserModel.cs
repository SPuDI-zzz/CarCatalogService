using AutoMapper;
using CarCatalogService.Data.Entities;

namespace CarCatalogService.Services.UserService.Models;

public class UpdateUserModel
{
    public required string Login { get; set; }

    public required string Role { get; set; }
}

public class UpdateUserModelProfile : Profile
{
    public UpdateUserModelProfile()
    {
        CreateMap<UpdateUserModel, User>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Login));
    }
}
