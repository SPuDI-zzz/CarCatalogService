using AutoMapper;
using CarCatalogService.Data.Entities;
using CarCatalogService.Services.UserService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarCatalogService.Services.UserService;

public class UserService : IUserSevice
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public UserService(IMapper mapper, UserManager<User> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task AddUser(AddUserModel model)
    {
        var user = _mapper.Map<User>(model);

        var resultCreateUser = await _userManager.CreateAsync(user, model.Password);
        if (!resultCreateUser.Succeeded)
            throw new Exception($"Creating user account is wrong" +
                $"{String.Join(", ", resultCreateUser.Errors.Select(e => e.Description))}");

        await _userManager.AddToRolesAsync(user, model.Roles);
    }

    public async Task DeleteUser(long userId)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(user => user.Id.Equals(userId))
            ?? throw new Exception($"The user (id: {userId}) was not found");

        await _userManager.DeleteAsync(user);
    }

    public async Task<UserModel?> GetUser(long userId)
    {
        var data = await _userManager.Users
            .Include(user => user.UserRoles)
                .ThenInclude(userRoles => userRoles.Role)
            .Select(user => new UserModel
            {
                Id = user.Id,
                Login = user.UserName!,
                Roles = user.UserRoles!.Select(userRoles => userRoles.Role.Name)!
            })
            .FirstOrDefaultAsync(user => user.Id.Equals(userId));

        return data;
    }

    public async Task<IEnumerable<UserModel>> GetAllUsers()
    {
        var data = await _userManager.Users
            .Include(user => user.UserRoles)
                .ThenInclude(userRoles => userRoles.Role)
            .Select(user => new UserModel
            {
                Id = user.Id,
                Login = user.UserName!,
                Roles = user.UserRoles!.Select(userRoles => userRoles.Role.Name)!
            })
            .ToListAsync();

        return data;
    }

    public async Task UpdateUser(long userId, UpdateUserModel model)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(user => user.Id.Equals(userId))
            ?? throw new Exception($"The user (id: {userId}) was not found");

        user = _mapper.Map(model, user);

        var resultUpdateUser = await _userManager.UpdateAsync(user);
        if (!resultUpdateUser.Succeeded)
            throw new Exception($"Updating user account is wrong" +
                $"{String.Join(", ", resultUpdateUser.Errors.Select(e => e.Description))}");

        var roles = await _userManager.GetRolesAsync(user!);

        if (!roles.SequenceEqual(model.Roles))
        {
            await _userManager.RemoveFromRolesAsync(user, roles);
            await _userManager.AddToRolesAsync(user, model.Roles);
        }        
    }
}
