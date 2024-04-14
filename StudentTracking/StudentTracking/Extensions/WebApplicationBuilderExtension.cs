using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using StudentTracking.DataManager;
using NLog.Web;
using StudentTracking.DataManager.Implementations.Main;
using StudentTracking.DataManager.Implementations.Supporting;
using StudentTracking.DataManager.Interfaces.Main;
using StudentTracking.DataManager.Interfaces.Supporting;
using StudentTracking.Service.Implementations.Main;
using StudentTracking.Service.Interfaces.Main;

namespace StudentTracking.Extensions;

public static class WebApplicationBuilderExtension
{
    public static void AddServices(this WebApplicationBuilder builder)
    {
        //Default
        builder.Services.AddControllersWithViews();
        
        //Repository
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IRoleRepository, RoleRepository>();
        builder.Services.AddScoped<IRoleUserRepository, RoleUserRepository>();

        //Services
        builder.Services.AddScoped<IRoleService, RoleService>();
        builder.Services.AddScoped<IUserService, UserService>();
    }
    public static void AddAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.Name = "Authentication";
                options.LoginPath = "/Account/Authorization";
                options.Cookie.HttpOnly = true;
                options.SlidingExpiration = true;
            });
    }
    
    public static void AddLogging(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        builder.Host.UseNLog();
    }
    public static void AddDataBase(this WebApplicationBuilder builder)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        string? dataBaseConnection = builder.Configuration.GetConnectionString("ConnectionString");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(dataBaseConnection);
        });
    }
}