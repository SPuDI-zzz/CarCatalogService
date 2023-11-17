using AutoMapper;
using CarCatalogService.Data.Entities;

namespace CarCatalogService.Services.AccountService.Models;

public class RegisterUserAccountModel
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public required IEnumerable<string> Roles { get; set; }
}

public class RegisterUserAccountModelProfile : Profile
{
    public RegisterUserAccountModelProfile()
    {
        CreateMap<RegisterUserAccountModel, User>();
    }
}
