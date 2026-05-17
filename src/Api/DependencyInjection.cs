//using Api.Services;
using System.Text.Json;
using Api.Common.JsonConverters;
using Api.Services;
using Infrastructure.LocalServices.BackgroundServices;
using Infrastructure.LocalServices.Hashing;
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
        services.AddScoped<ICurrentUserService, CurrentUserContextService>();
        services.AddScoped<ICartIdentityService, CartIdentityService>();


        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.Converters.Add(new LongAsStringJsonConverter());
        });
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
                        options.ExpireTimeSpan = TimeSpan.FromDays(7);
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
