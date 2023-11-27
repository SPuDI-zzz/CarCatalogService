using CarCatalogService.BLL.Services.AccountService;
using CarCatalogService.BLL.Services.CarService;
using CarCatalogService.BLL.Services.UserService;
using CarCatalogService.DAL.Entities;
using CarCatalogService.DAL.EntityFramework;
using CarCatalogService.DAL.EntityFramework.Setup;
using CarCatalogService.DAL.Repositories.CarRepository;
using CarCatalogService.DAL.Repositories.Interfaces;
using CarCatalogService.Middlewares;
using CarCatalogService.Shared.Const;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using NLog;
using NLog.Web;
using CarCatalogService.Filters;

var logger = LogManager
    .Setup()
    .LoadConfigurationFromAppSettings()
    .GetCurrentClassLogger();

logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);
    var services = builder.Services;
    var configuration = builder.Configuration;

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    services.AddControllersWithViews(options => {
        options.Filters.Add<RequestLogResourceFilter>();
    });

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
    });

    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    services.AddScoped<ICarService, CarService>();
    services.AddScoped<IUserSevice, UserService>();
    services.AddScoped<IRepository<Car>, CarRepository>();
    services.AddScoped<IAccountService, AccountService>();

    var app = builder.Build();

    app.UseMiddleware<ErrorRedirectorMiddleware>();
    app.UseMiddleware<ExceptionsMiddleware>();

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
}
catch(Exception ex)
{
    logger.Error(ex);
}
finally
{
    LogManager.Shutdown();
}
