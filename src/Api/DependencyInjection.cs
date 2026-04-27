//using Api.Services;
using Api.Services;
using Application.Common.Identity;
using Infrastructure.LocalServices.HashingService;
using Microsoft.AspNetCore.Identity.UI.Services;


namespace Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration config, IWebHostEnvironment env)
    {


        services.AddCustomServices()
                
                .AddHttpContextAccessor()
                .AddOpenApiAndScalarServices()
                .AddAuthenticationServices()
                .AddAuthorizationServices();

        return services;
    }

    private static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        // for simple dependency injection Transient/Singleton/Scoped
        services.AddTransient<IEmailSender, EmailSenderFaker>();
        services.AddScoped<IUserContext, UserContext>();
        services.AddControllers();
        return services;
    }

    private static IServiceCollection AddOpenApiAndScalarServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddOpenApi();
        return services;
    }


    private static IServiceCollection AddAuthenticationServices(this IServiceCollection services)
    {
        services.AddAuthentication()
                .AddCookie(IdentityConstants.ApplicationScheme, options => {

                        options.AccessDeniedPath = "/Account/AccessDenied";
                        options.SlidingExpiration = true;
                        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                })

;
        return services;
    }

    private static IServiceCollection AddAuthorizationServices(this IServiceCollection services)
    {
        services.AddAuthorization();
        return services;
    }
}
