
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddCustomServices(config);


        return services;
    }

    private static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration config)
    {
        // for simple dependency injection Transient/Singleton/Scoped
        services.AddMediatR((m) => { m.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly); });
        return services;
    }

}
