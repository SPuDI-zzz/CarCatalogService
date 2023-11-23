using Microsoft.EntityFrameworkCore;

namespace CarCatalogService.DAL.EntityFramework.Setup;

/// <summary>
///     Static class for initializing the database during application startup.
/// </summary>
public static class DbInitializer
{
    /// <summary>
    ///     Executes database initialization tasks.
    /// </summary>
    /// <param name="serviceProvider">The service provider for accessing required services.</param>
    /// <remarks>
    ///     This initializer is responsible for applying any pending database migrations during application startup.
    ///     It uses the provided <paramref name="serviceProvider"/> to obtain the <see cref="MainDbContext"/> using
    ///     a scoped service scope. The <see cref="MainDbContext"/> is then used to apply any pending migrations
    ///     to the database using the <see cref="DbContext.Database.Migrate"/> method.
    /// </remarks>
    public static void Execute(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.GetService<IServiceScopeFactory>()?.CreateScope();
        ArgumentNullException.ThrowIfNull(scope);

        var dbContextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<MainDbContext>>();
        using var context = dbContextFactory.CreateDbContext();
        context.Database.Migrate();
    }
}
