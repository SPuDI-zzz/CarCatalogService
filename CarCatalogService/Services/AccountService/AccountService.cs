using AutoMapper;
using CarCatalogService.Data;
using CarCatalogService.Data.Entities;
using CarCatalogService.Services.AccountService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarCatalogService.Services.AccountService;

public class AccountService : IAccountService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<UserRole> _roleManager;
    private readonly IDbContextFactory<MainDbContext> _contextFactory;
    private readonly IMapper _mapper;

    public AccountService(
        UserManager<User> userManager,
        IDbContextFactory<MainDbContext> contextFactory,
        IMapper mapper,
        RoleManager<UserRole> roleManager)
    {
        _userManager = userManager;
        _contextFactory = contextFactory;
        _mapper = mapper;
        _roleManager = roleManager;
    }

    public async Task<UserAccountModel> Register(RegisterUserAccountModel model)
    {
        var user = await _userManager.FindByNameAsync(model.UserName);

        if (user != null)
            throw new Exception($"A user with username {model.UserName} already exist");

        user = _mapper.Map<User>(model);

        var resultCreateUser = await _userManager.CreateAsync(user);

        if (!resultCreateUser.Succeeded)
            throw new Exception($"Creating user account is wrong. " +
                $"{String.Join(", ", resultCreateUser.Errors.Select(s => s.Description))}");

        var resultAddRoles = await _userManager.AddToRolesAsync(user, model.Roles);

        if (!resultAddRoles.Succeeded)
            throw new Exception($"Creating user account is wrong. " +
                $"{String.Join(", ", resultAddRoles.Errors.Select(s => s.Description))}");

        var data = _mapper.Map<UserAccountModel>(user);
        return data;
    }

    public async Task Login(LoginUserAccountModel model)
    {
        var user = await _userManager.FindByNameAsync(model.UserName)
            ?? throw new Exception($"There is no user with this username {model.UserName}");

        var checkPassword = await _userManager.CheckPasswordAsync(user, model.Password);

    }
}
