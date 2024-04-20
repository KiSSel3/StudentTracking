using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using StudentTracking.DataManager;
using NLog.Web;
using StudentTracking.DataManager.Implementations.Contract;
using StudentTracking.DataManager.Implementations.Letter;
using StudentTracking.DataManager.Implementations.Main;
using StudentTracking.DataManager.Implementations.Shared;
using StudentTracking.DataManager.Implementations.Supporting;
using StudentTracking.DataManager.Interfaces;
using StudentTracking.DataManager.Interfaces.Contract;
using StudentTracking.DataManager.Interfaces.Letter;
using StudentTracking.DataManager.Interfaces.Main;
using StudentTracking.DataManager.Interfaces.Supporting;
using StudentTracking.Domain.Entities.Shared;
using StudentTracking.Service.Implementations.Letter;
using StudentTracking.Service.Implementations.Main;
using StudentTracking.Service.Implementations.Shared;
using StudentTracking.Service.Interfaces.Letter;
using StudentTracking.Service.Interfaces.Main;
using StudentTracking.Service.Interfaces.Shared;
using StudentTracking.Shared.Mappers;

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
        
        builder.Services.AddScoped<IAnnualNumberPeopleRepository, AnnualNumberPeopleRepository>();
        builder.Services.AddScoped<IContractRepository, ContractRepository>();
        builder.Services.AddScoped<ICountRepository, CountRepository>();
        builder.Services.AddScoped<ILetterRepository, LetterRepository>();
        builder.Services.AddScoped<IRemoteAreaRepository, RemoteAreaRepository>();
        builder.Services.AddScoped<ISpecialtyRepository, SpecialtyRepository>();
        builder.Services.AddScoped<IStudentRepository, StudentRepository>();
        builder.Services.AddScoped<IBaseRepository<CompanyEntity>, CompanyRepository>();
        builder.Services.AddScoped<IBaseRepository<FacultyEntity>, FacultyRepository>();

        //Services
        builder.Services.AddScoped<IRoleService, RoleService>();
        builder.Services.AddScoped<IUserService, UserService>();

        builder.Services.AddScoped<ILetterService, LetterService>();
        
        builder.Services.AddScoped<ICompanyService, CompanyService>();
        builder.Services.AddScoped<IFacultyService, FacultyService>();
    }

    public static void AddMappers(this WebApplicationBuilder builder)
    {
        /*builder.Services.AddScoped<ContractToFullContractMapper>();
        builder.Services.AddScoped<LetterToFullLetterMapper>();*/
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