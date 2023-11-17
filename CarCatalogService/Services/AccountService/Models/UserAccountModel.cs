using AutoMapper;
using CarCatalogService.Data.Entities;

namespace CarCatalogService.Services.AccountService.Models;

public class UserAccountModel
{
    public long Id { get; set; }
    public required string UserName { get; set; }
}

public class UserAccountModelProfile : Profile
{
    public UserAccountModelProfile()
    {
        CreateMap<User, UserAccountModel>();
    }
}
