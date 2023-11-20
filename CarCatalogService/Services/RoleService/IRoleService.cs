using CarCatalogService.Services.RoleService.Models;

namespace CarCatalogService.Services.RoleService;

public interface IRoleService
{ 
    Task<IEnumerable<RoleModel>> GetAllRoles();
}
