using AutoMapper;
using CarCatalogService.Data.Entities;

namespace CarCatalogService.Services.RoleService.Models;

public class UpdateRoleModel
{
    public required string Name { get; set; }
}

public class UpdateRoleModelProfile : Profile
{
    public UpdateRoleModelProfile()
    {
        CreateMap<UpdateRoleModel, UserRole>();
    }
}
