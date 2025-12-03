
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddCustomServices(config)
                .AddUserAccountOptionsService(config);


        return services;
    }

    private static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration config)
    {
        // for simple dependency injection Transient/Singleton/Scoped

        return services;
    }

    private static IServiceCollection AddUserAccountOptionsService(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<UserAccountSettings>(config.GetSection("UserAccountOptions"));

        return services;
    }
}
