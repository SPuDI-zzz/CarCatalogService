using CarCatalogService.Data.Entities;
using CarCatalogService.Data.EntityFramework;
using CarCatalogService.Data.EntityFramework.Setup;
using CarCatalogService.Data.Repositories.CarRepository;
using CarCatalogService.Data.Repositories.Interfaces;
using CarCatalogService.Services.AccountService;
using CarCatalogService.Services.CarService;
using CarCatalogService.Services.UserService;
using CarCatalogService.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddControllersWithViews();

services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddCookie(opt => opt.Cookie.Name = "token")
.AddJwtBearer("jwt", options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = configuration["Jwt:Audience"],
        ValidIssuer = configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
    };
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            context.Token = context.Request.Cookies["token"];
            return Task.CompletedTask;
        }
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

services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .AddAuthenticationSchemes(IdentityConstants.ApplicationScheme, "jwt")
    .Build();

    options.AddPolicy(AppRoles.User, policy => policy
    .RequireAuthenticatedUser()
    .AddAuthenticationSchemes(IdentityConstants.ApplicationScheme, "jwt")
    .RequireRole(AppRoles.User, AppRoles.Manager, AppRoles.Admin));

    options.AddPolicy(AppRoles.Manager, policy => policy
    .RequireAuthenticatedUser()
    .AddAuthenticationSchemes(IdentityConstants.ApplicationScheme, "jwt")
    .RequireRole(AppRoles.Manager, AppRoles.Admin));

    options.AddPolicy(AppRoles.Admin, policy => policy
    .RequireAuthenticatedUser()
    .AddAuthenticationSchemes(IdentityConstants.ApplicationScheme, "jwt")
    .RequireRole(AppRoles.Admin));

    options.AddPolicy("AllowAnonymousPolicy", policy => policy
    .RequireAuthenticatedUser()
    .AddAuthenticationSchemes(IdentityConstants.ApplicationScheme, "jwt")
    .RequireAssertion(context => true));
});

services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

services.AddScoped<ICarService, CarService>();
services.AddScoped<IUserSevice, UserService>();
services.AddScoped<IRepository<Car>, CarRepository>();
services.AddScoped<IAccountService, AccountService>();

var app = builder.Build();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

DbInitializer.Execute(app.Services);
DbSeeder.Execute(app.Services, true, true);

app.Run();
