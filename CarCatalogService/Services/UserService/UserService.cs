using AutoMapper;
using CarCatalogService.Data;
using CarCatalogService.Data.Entities;
using CarCatalogService.Services.UserService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarCatalogService.Services.UserService;

public class UserService : IUserSevice
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<UserRole> _roleManager;
    private readonly IMapper _mapper;
    private readonly IDbContextFactory<MainDbContext> _contextFactory;

    public UserService(IMapper mapper, UserManager<User> userManager, RoleManager<UserRole> roleManager, IDbContextFactory<MainDbContext> contextFactory)
    {
        _mapper = mapper;
        _userManager = userManager;
        _roleManager = roleManager;
        _contextFactory = contextFactory;
    }

    public async Task<UserModel> AddUser(AddUserModel model)
    {
        var user = _mapper.Map<User>(model);

        var resultCreateUser = await _userManager.CreateAsync(user, model.Password);
        if (!resultCreateUser.Succeeded)
            throw new Exception($"Creating user account is wrong" +
                $"{String.Join(", ", resultCreateUser.Errors.Select(s => s.Description))}");

        var role = (await _roleManager.Roles.FirstAsync(role => role.Name!.Equals(model.Role)));
        await _userManager.AddToRoleAsync(user, model.Role);

        var data = _mapper.Map<UserModel>(user);
        data = _mapper.Map(role, data);

        return data;
    }

    public async Task DeleteUser(long userId)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(user => user.Id.Equals(userId))
            ?? throw new Exception($"The user (id: {userId}) was not found");

        await _userManager.DeleteAsync(user);
    }

    public async Task<UserModel> GetUser(long userId)
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        var userWithRole = await context.UserRoles
            .Include(val => val.User)
            .Include(val => val.Role)
            .Where(val => val.UserId == userId)
            .FirstOrDefaultAsync();

        var data = _mapper.Map<UserModel>(userWithRole);
        return data;
    }

    public async Task<IEnumerable<UserModel>> GetAllUsers()
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        var a = await context.Users
            .Include(val => val.UserRoles)
                .ThenInclude(val => val.Role)
            .Select(val => new TempUserModel
            {
                Id = val.Id,
                Login = val.UserName!,
                Roles = val.UserRoles!.Select(val => val.Role.Name)!
            }).ToListAsync();

        var usersWithRoles = context.UserRoles
            .Include(val => val.User)
            .Include(val => val.Role)
            .Select(_mapper.Map<UserModel>)//val => new UserModel { Id = val.UserId, Login = val.User.UserName, Role = val.Role.Name })
            .ToList();

        //var data = usersWithRoles.Select(_mapper.Map<UserModel>);
        return usersWithRoles;
    }

    public async Task<UserModel> UpdateUser(long userId, UpdateUserModel model)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(user => user.Id.Equals(userId))
            ?? throw new Exception($"The user (id: {userId}) was not found");

        user = _mapper.Map(model, user);

        var roles = await _userManager.GetRolesAsync(user!);
        var data = _mapper.Map<UserModel>(user);

        await _userManager.UpdateAsync(user);

        if (!roles.First().Equals(model.Role))
        {
            await _userManager.RemoveFromRoleAsync(user, roles[0]);
            await _userManager.AddToRoleAsync(user, model.Role);
        }

        data.Role = model.Role;
        return data;
    }
}
