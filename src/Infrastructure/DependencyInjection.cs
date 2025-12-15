
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config, IWebHostEnvironment enviroment)
    {
        services.AddCustomServices()
                .AddDatabaseService(config, enviroment)





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

    private static IServiceCollection AddDatabaseService(this IServiceCollection services, IConfiguration config, IWebHostEnvironment enviroment)
    {
        string connString = string.Empty;

        if(enviroment.IsProduction())
            connString = config["CONNECTION_STRING"] ?? throw new ArgumentNullException("Environment variable 'CONNECTION_STRING' not found.");
        else if (enviroment.IsStaging())
            connString = Environment.GetEnvironmentVariable("CONNECTION_STRING", EnvironmentVariableTarget.User) ?? throw new ArgumentNullException("Environment variable 'CONNECTION_STRING' not found.");
        else
            connString = config.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException("Connection string 'Default' not found.");

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(connString);
        });

        services.AddScoped<IAppDbContext, AppDbContext>();
        return services;
    }


}
