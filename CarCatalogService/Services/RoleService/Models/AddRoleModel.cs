using AutoMapper;
using CarCatalogService.Data.Entities;

namespace CarCatalogService.Services.RoleService.Models;

public class AddRoleModel
{
    public required string Name { get; set; }
}

public class AddRoleModelProfile : Profile
{
    public AddRoleModelProfile()
    {
        CreateMap<AddRoleModel, Role>();
    }
}
