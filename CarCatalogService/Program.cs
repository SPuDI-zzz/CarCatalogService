using CarCatalogService.Data;
using CarCatalogService.Services.CarSercice;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddDbContextFactory<MainDbContext>(options =>
{   
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.EnableSensitiveDataLogging();
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
services.AddSingleton<ICarService, CarService>();

services.AddControllersWithViews();

var app = builder.Build();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Cars}/{action=Index}/{id?}");

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
