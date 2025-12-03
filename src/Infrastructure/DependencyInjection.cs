
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddCustomServices()
                .AddDatabaseService(config)





                ;

        return services;
    }

    private static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        // for simple dependency injection Transient/Singleton/Scoped
        services.AddSingleton(TimeProvider.System);
        services.AddScoped<IPasswordHasher<User>, AppPasswordHasher>();

        return services;
    }

    private static IServiceCollection AddDatabaseService(this IServiceCollection services, IConfiguration config)
    {
        string connString = config.GetConnectionString("Default") ?? throw new ArgumentNullException("Connection string 'Default' not found.");

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(connString);
        });

        services.AddScoped<IAppDbContext, AppDbContext>();
        return services;
    }


}
