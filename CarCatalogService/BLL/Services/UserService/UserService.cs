using AutoMapper;
using CarCatalogService.BLL.Services.UserService.Models;
using CarCatalogService.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarCatalogService.BLL.Services.UserService;

/// <summary>
///     Provides functionality for managing user-related operations.
/// </summary>
public class UserService : IUserSevice
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    /// <summary>
    ///     Initializes a new instance of the <see cref="UserService"/> class.
    /// </summary>
    /// <param name="mapper">An <see cref="IMapper"/> instance for object mapping.</param>
    /// <param name="userManager">A <see cref="UserManager{TUser}"/> instance for managing user-related operations.</param>
    public UserService(IMapper mapper, UserManager<User> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    /// <inheritdoc/>
    public async Task<AddUserAccountResponseModel> AddUserAsync(AddUserModel model)
    {
        var user = _mapper.Map<User>(model);

        var resultCreateUser = await _userManager.CreateAsync(user, model.Password);
        if (!resultCreateUser.Succeeded)
            return new AddUserAccountResponseModel
            {
                IsError = true,
                ErrorMessage = $"Creating user account is wrong" +
                    $"{string.Join(", ", resultCreateUser.Errors.Select(e => e.Description))}"
            };

        await _userManager.AddToRolesAsync(user, model.Roles);
        return new();
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteUserAsync(long userId)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(user => user.Id.Equals(userId));
        if (user == null)
            return false;

        await _userManager.DeleteAsync(user);
        return true;
    }

    /// <inheritdoc/>
    public async Task<UserModel?> GetUserAsync(long userId)
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

    /// <inheritdoc/>
    public async Task<IEnumerable<UserModel>> GetAllUsersAsync()
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

    /// <inheritdoc/>
    public async Task<UpdateUserAccountResponseModel> UpdateUserAsync(long userId, UpdateUserModel model)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(user => user.Id.Equals(userId));
        if (user == null)
            return new UpdateUserAccountResponseModel
            {
                IsError = true,
                ErrorMessage = $"The user (id: {userId}) was not found"
            };

        user = _mapper.Map(model, user);

        var resultUpdateUser = await _userManager.UpdateAsync(user);
        if (!resultUpdateUser.Succeeded)
            return new UpdateUserAccountResponseModel
            {
                IsError = true,
                ErrorMessage = $"Updating user account is wrong" +
                    $"{string.Join(", ", resultUpdateUser.Errors.Select(e => e.Description))}"
            };

        var roles = await _userManager.GetRolesAsync(user);

        if (!roles.SequenceEqual(model.Roles))
        {
            await _userManager.RemoveFromRolesAsync(user, roles);
            await _userManager.AddToRolesAsync(user, model.Roles);
        }
        return new();
    }
}
