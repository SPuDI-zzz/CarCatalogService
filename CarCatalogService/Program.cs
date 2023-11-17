using CarCatalogService.Data;
using CarCatalogService.Data.Entities;
using CarCatalogService.Data.Setup;
using CarCatalogService.Services.AccountService;
using CarCatalogService.Services.CarService;
using CarCatalogService.Services.RoleService;
using CarCatalogService.Services.UserService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = configuration["Jwt:Audience"],
        ValidIssuer = configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!))
    };
});

services.AddDbContextFactory<MainDbContext>(options =>
{   
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
    options.EnableSensitiveDataLogging();
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

services.AddIdentity<User, UserRole>()
    .AddEntityFrameworkStores<MainDbContext>()
    .AddDefaultTokenProviders();

services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
services.AddScoped<ICarService, CarService>();
services.AddScoped<IRoleService, RoleService>();
services.AddScoped<IUserSevice, UserService>();
services.AddScoped<IAccountService, AccountService>();

services.AddControllersWithViews();

var app = builder.Build();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

DbInitializer.Execute(app.Services);

app.Run();
