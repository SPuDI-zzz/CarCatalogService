using CarCatalogService.Data;
using CarCatalogService.Services.CarService;
using CarCatalogService.Services.RoleService;
using CarCatalogService.Services.UserService;
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
services.AddScoped<ICarService, CarService>();
services.AddScoped<IRoleService, RoleService>();
services.AddScoped<IUserSevice, UserService>();

services.AddControllersWithViews();

var app = builder.Build();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
