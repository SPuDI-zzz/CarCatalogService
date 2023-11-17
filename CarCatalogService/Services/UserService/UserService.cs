using AutoMapper;
using CarCatalogService.Data;
using CarCatalogService.Data.Entities;
using CarCatalogService.Services.UserService.Models;
using Microsoft.EntityFrameworkCore;

namespace CarCatalogService.Services.UserService;

public class UserService : IUserSevice
{
    private readonly IDbContextFactory<MainDbContext> _contextFactory;
    private readonly IMapper _mapper;

    public UserService(IDbContextFactory<MainDbContext> contextFactory, IMapper mapper)
    {
        _contextFactory = contextFactory;
        _mapper = mapper;
    }

    public async Task<UserModel> AddUser(AddUserModel model)
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        var user = _mapper.Map<User>(model);

        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        var data = _mapper.Map<UserModel>(user);
        return data;
    }

    public async Task DeleteUser(long userId)
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        var user = await context.Users.FirstOrDefaultAsync(user => user.Id.Equals(userId))
            ?? throw new Exception($"The user (id: {userId}) was not found");

        context.Remove(user);
        context.SaveChanges();
    }

    public async Task<UserModel> GetUser(long userId)
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        var user = await context.Users.FirstOrDefaultAsync(user => user.Id.Equals(userId));

        var data = _mapper.Map<UserModel>(user);
        return data;
    }

    public async Task<IEnumerable<UserModel>> GetAllUsers()
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        var data = (await context.Users.ToListAsync())
            .Select(_mapper.Map<UserModel>);

        return data;
    }

    public async Task<UserModel> UpdateUser(long userId, UpdateUserModel model)
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        var user = await context.Users.FirstOrDefaultAsync(user => user.Id.Equals(userId))
            ?? throw new Exception($"The user (id: {userId}) was not found");

        user = _mapper.Map(model, user);

        context.Users.Update(user);
        await context.SaveChangesAsync();

        var data = _mapper.Map<UserModel>(user);
        return data;
    }
}
