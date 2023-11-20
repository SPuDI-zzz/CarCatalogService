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

    public async Task<IEnumerable<RoleModel>> GetAllRoles()
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        var data = (await context.Roles.ToListAsync())
            .Select(_mapper.Map<RoleModel>);

        return data;
    }
}
