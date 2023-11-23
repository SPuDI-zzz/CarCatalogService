using CarCatalogService.BLL.Services.UserService;
using CarCatalogService.DAL.Entities;
using CarCatalogService.Shared.Const;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarCatalogService.DAL.EntityFramework.Setup;

/// <summary>
///      class for seeding initial data into the database during application startup.
/// </summary>
public static class DbSeeder
{
    /// <summary>
    ///     Helper method to create a scoped service scope from the provided <see cref="IServiceProvider"/>.
    /// </summary>
    /// <param name="serviceProvider">The <see cref="IServiceProvider"/> from which to create the service scope.</param>
    /// <returns>A scoped service scope.</returns>
    private static IServiceScope ServiceScope(IServiceProvider serviceProvider) => serviceProvider.GetService<IServiceScopeFactory>()!.CreateScope();

    /// <summary>
    ///     Helper method to obtain an instance of the <see cref="IUserSevice"/> from the provided <see cref="IServiceProvider"/>.
    /// </summary>
    /// <param name="serviceProvider">The <see cref="IServiceProvider"/> from which to obtain the user service.</param>
    /// <returns>An instance of the <see cref="IUserSevice"/>.</returns>
    private static IUserSevice UserService(IServiceProvider serviceProvider) => ServiceScope(serviceProvider).ServiceProvider.GetRequiredService<IUserSevice>();

    /// <summary>
    ///     Helper method to obtain an instance of the <see cref="MainDbContext"/> from the provided <see cref="IServiceProvider"/>.
    /// </summary>
    /// <param name="serviceProvider">The <see cref="IServiceProvider"/> from which to obtain the database context.</param>
    /// <returns>An instance of the <see cref="MainDbContext"/>.</returns>
    private static MainDbContext DbContext(IServiceProvider serviceProvider) => ServiceScope(serviceProvider).ServiceProvider.GetRequiredService<IDbContextFactory<MainDbContext>>().CreateDbContext();

    /// <summary>
    ///     Helper method to obtain an instance of the <see cref="RoleManager{UserRole}"/> from the provided <see cref="IServiceProvider"/>.
    /// </summary>
    /// <param name="serviceProvider">The <see cref="IServiceProvider"/> from which to obtain the role manager.</param>
    /// <returns>An instance of the <see cref="RoleManager{UserRole}"/>.</returns>
    private static RoleManager<UserRole> RoleManager(IServiceProvider serviceProvider) => ServiceScope(serviceProvider).ServiceProvider.GetRequiredService<RoleManager<UserRole>>();

    /// <summary>
    ///     Constant representing the default login name for the administrator user.
    /// </summary>
    private const string Login = "Admin";

    /// <summary>
    ///     Constant representing the default password for the administrator user.
    /// </summary>
    private const string Password = "Pass123#";

    /// <summary>
    /// Executes the database seeding process during application startup.
    /// </summary>
    /// <param name="serviceProvider">The <see cref="IServiceProvider"/> providing access to required services.</param>
    /// <param name="addDemoData">A flag indicating whether to add demo data to the database.</param>
    /// <param name="addAdmin">A flag indicating whether to add an administrator user to the database.</param>
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

    /// <summary>
    ///     Helper method to configure user roles in the application.
    /// </summary>
    /// <param name="serviceProvider">The <see cref="IServiceProvider"/> providing access to required services.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
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

    /// <summary>
    ///     Helper method to configure the administrator user in the application.
    /// </summary>
    /// <param name="serviceProvider">The <see cref="IServiceProvider"/> providing access to required services.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    private static async Task ConfigureAdministrator(IServiceProvider serviceProvider)
    {
        var userService = UserService(serviceProvider);

        var isZeroAdminUsers = !(await userService.GetAllUsersAsync())
            .Where(user => user.Roles
                .Where(role => role.Equals(AppRoles.Admin))
                .Any())
            .Any();

        if (isZeroAdminUsers)
        {
            await userService.AddUserAsync(new()
            {
                Login = Login,
                Password = Password,
                Roles = new[] { AppRoles.Admin }
            });
        }
    }

    /// <summary>
    ///     Helper method to configure demo data in the application.
    /// </summary>
    /// <param name="serviceProvider">The <see cref="IServiceProvider"/> providing access to required services.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
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
