using CarCatalogService.Services.UserService.Models;

namespace CarCatalogService.Services.UserService;

public interface IUserSevice
{
    Task<UserModel?> GetUser(long userId);
    Task<IEnumerable<UserModel>> GetAllUsers();
    Task AddUser(AddUserModel model);
    Task UpdateUser(long userId, UpdateUserModel model);
    Task DeleteUser(long userId);
}
