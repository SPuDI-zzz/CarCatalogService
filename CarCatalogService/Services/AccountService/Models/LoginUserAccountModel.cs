using AutoMapper;
using CarCatalogService.Data.Entities;

namespace CarCatalogService.Services.AccountService.Models;

public class LoginUserAccountModel
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
}

public class LoginUserAccountModelProfile : Profile
{
    public LoginUserAccountModelProfile()
    {
        CreateMap<LoginUserAccountModel, User>();
    }
}
