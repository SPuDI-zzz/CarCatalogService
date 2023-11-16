using CarCatalogService.Services.RoleService.Models;
using CarCatalogService.Services.UserService.Models;

namespace CarCatalogService.Views.Users.Models;

public class UserModelView
{
    public IEnumerable<UserModel> Users { get; set; }
    public IEnumerable<RoleModel> Roles { get; set; }
}
