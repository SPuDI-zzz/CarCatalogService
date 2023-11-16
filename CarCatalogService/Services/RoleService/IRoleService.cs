using CarCatalogService.Services.RoleService.Models;

namespace CarCatalogService.Services.RoleService;

public interface IRoleService
{
    Task<RoleModel> AddRole(AddRoleModel model);
    Task Delete(long roleId);
    Task<RoleModel> GetRole(long roleId);
    Task<IEnumerable<RoleModel>> GetAllRoles();
    Task<RoleModel> UpdateRole(long roleId, UpdateRoleModel model);
}
