using AutoMapper;
using CarCatalogService.Data;
using CarCatalogService.Data.Entities;
using CarCatalogService.Services.RoleService.Models;
using Microsoft.EntityFrameworkCore;

namespace CarCatalogService.Services.RoleService;

public class RoleService : IRoleService
{
    private readonly IDbContextFactory<MainDbContext> _contextFactory;
    private readonly IMapper _mapper;

    public RoleService(IDbContextFactory<MainDbContext> contextFactory, IMapper mapper)
    {
        _contextFactory = contextFactory;
        _mapper = mapper;
    }

    public async Task<RoleModel> AddRole(AddRoleModel model)
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        var role = _mapper.Map<Role>(model);

        await context.Roles.AddAsync(role);
        await context.SaveChangesAsync();

        var data = _mapper.Map<RoleModel>(role);
        return data;
    }

    public async Task Delete(long roleId)
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        var role = await context.Roles.FirstOrDefaultAsync(id => id.Equals(roleId))
            ?? throw new Exception($"The role (id: {roleId}) was not found");
    }

    public async Task<IEnumerable<RoleModel>> GetAllRoles()
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        var data = (await context.Roles.ToListAsync())
            .Select(_mapper.Map<RoleModel>);

        return data;
    }

    public async Task<RoleModel> GetRole(long roleId)
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        var role = await context.Roles.FirstOrDefaultAsync(id => id.Equals(roleId));

        var data = _mapper.Map<RoleModel>(role);
        return data;
    }

    public async Task<RoleModel> UpdateRole(long roleId, UpdateRoleModel model)
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        var role = await context.Roles.FirstOrDefaultAsync(id => id.Equals(roleId))
            ?? throw new Exception($"The role (id: {roleId}) was not found");

        role = _mapper.Map(model, role);

        context.Roles.Update(role);
        await context.SaveChangesAsync();

        var data = _mapper.Map<RoleModel>(role);
        return data;
    }
}
