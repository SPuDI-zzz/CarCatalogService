using CarCatalogService.Services.AccountService.Models;

namespace CarCatalogService.Services.AccountService;

public interface IAccountService
{
    Task<UserAccountModel> Register(RegisterUserAccountModel model);
    Task<string> Login(LoginUserAccountModel model);
}
