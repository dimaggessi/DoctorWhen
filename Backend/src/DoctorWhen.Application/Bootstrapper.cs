using DoctorWhen.Application.Interfaces;
using DoctorWhen.Application.Services;
using DoctorWhen.Application.Utilities.Token;
using DoctorWhen.Persistence.Repository.RepositoryAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DoctorWhen.Application;
public static class Bootstrapper
{
    public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
    {
        AddServices(services);
        AddIdentity(services, configuration);
        AddAuthentication(services, configuration);
    }


    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenConfigurator, TokenConfigurator>();
        
    }

    private static void AddIdentity(IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentityCore<Domain.Identity.User>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 6;
        })
            .AddRoles<Domain.Identity.Role>()
            .AddRoleManager<RoleManager<Domain.Identity.Role>>()
            .AddSignInManager<SignInManager<Domain.Identity.User>>()
            .AddRoleValidator<RoleValidator<Domain.Identity.Role>>()
            .AddEntityFrameworkStores<DoctorWhenContext>()

            // Possibilita o reset do Token no UserService
            .AddDefaultTokenProviders();
    }

    private static void AddAuthentication(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetRequiredSection("Configurations:Jwt:Token").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
    }
}
