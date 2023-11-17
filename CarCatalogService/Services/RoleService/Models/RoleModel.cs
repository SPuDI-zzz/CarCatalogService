using AutoMapper;
using CarCatalogService.Data.Entities;

namespace CarCatalogService.Services.RoleService.Models;

public class RoleModel
{
    public long Id { get; set; }
    public required string Name { get; set; }
}

public class RoleModelProfile : Profile
{
    public RoleModelProfile()
    {
        CreateMap<UserRole, RoleModel>();
    }
}
