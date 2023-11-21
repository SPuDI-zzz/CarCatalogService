using CarCatalogService.Data.Entities;
using CarCatalogService.Services.UserService;
using CarCatalogService.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarCatalogService.Data.Setup;

public static class DbSeeder
{
    private static IServiceScope ServiceScope(IServiceProvider serviceProvider) => serviceProvider.GetService<IServiceScopeFactory>()!.CreateScope();
    private static IUserSevice UserService(IServiceProvider serviceProvider) => ServiceScope(serviceProvider).ServiceProvider.GetRequiredService<IUserSevice>();
    private static MainDbContext DbContext(IServiceProvider serviceProvider) => ServiceScope(serviceProvider).ServiceProvider.GetRequiredService<IDbContextFactory<MainDbContext>>().CreateDbContext();
    private static RoleManager<UserRole> RoleManager(IServiceProvider serviceProvider) => ServiceScope(serviceProvider).ServiceProvider.GetRequiredService<RoleManager<UserRole>>();

    private const string Login = "Admin";
    private const string Password = "Pass123#";

    private static async Task ConfigureRoles(IServiceProvider serviceProvider)
    {
        using var roleManager = RoleManager(serviceProvider);

        if (!roleManager.Roles.Where(role => role.Name!.Equals(AppRoles.User)).Any())
        {
            var user = new UserRole
            {
                Name = "User",
            };
            await roleManager.CreateAsync(user);
        }
        if (!roleManager.Roles.Where(role => role.Name!.Equals(AppRoles.Manager)).Any())
        {
            var manager = new UserRole
            {
                Name = "Manager",
            };
            await roleManager.CreateAsync(manager);
        }
        if (!roleManager.Roles.Where(role => role.Name!.Equals(AppRoles.Admin)).Any())
        {
            var admin = new UserRole
            {
                Name = "Admin",
            };
            await roleManager.CreateAsync(admin);
        }
    }

    private static async Task ConfigureAdministrator(IServiceProvider serviceProvider)
    {
        var userService = UserService(serviceProvider);
        //var test = await userService.GetAllUsers();
        //var userRole = test.Select(user => user.Roles);
        var isZeroAdminUsers = !(await userService.GetAllUsers())
            .Where(user => user.Roles
                .Where(role => role.Equals(AppRoles.Admin)).Any()).Any();

        if (isZeroAdminUsers)
        {
            await userService.AddUser(new()
            {
                Login = Login,
                Password = Password,
                Roles = new[] { AppRoles.Admin }
            });
        }
    }

    public static async void Execute(IServiceProvider serviceProvider, bool addDemoData, bool addAdmin = true)
    {
        await ConfigureRoles(serviceProvider);

        if (addAdmin)
        {
            await ConfigureAdministrator(serviceProvider);
        }

        if (addDemoData)
        {
            await ConfigureDemoData(serviceProvider);
        }
    }

    private static async Task ConfigureDemoData(IServiceProvider serviceProvider)
    {
        await using var context = DbContext(serviceProvider);

        if (context.Cars.Any() || !context.Users.Any())
            return;

        var user = await context.Users.FirstAsync();

        var car1 = new Car
        {
            Mark = "BMW",
            Model = "X5",
            Color = "Green",
            UserId = user.Id,
        };
        await context.Cars.AddAsync(car1);
        var car2 = new Car
        {
            Mark = "Lada",
            Model = "Granta",
            Color = "White",
            UserId = user.Id,
        };
        await context.Cars.AddAsync(car2);
        await context.SaveChangesAsync();
    }
}
