
using IdGen;
using Infrastructure.IdGenerators;
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
                .AddIdGenServices(config)
                .AddIdProviderServices()



                ;

        return services;
    }

    private static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        // for simple dependency injection Transient/Singleton/Scoped
        services.AddSingleton(TimeProvider.System);
        
        services.AddScoped<IPasswordHasher<User>, UserPasswordHasher>();

        return services;
    }

    private static IServiceCollection AddIdProviderServices(this IServiceCollection services)
    {
        services.AddKeyedSingleton<IIdProvider<long>, SnowflakeIdProvider>(IdProviderTypes.Snowflake);
        services.AddKeyedSingleton<IIdProvider<Guid>, GuidVersion7IdProvider>(IdProviderTypes.GuidVersion7);

        return services;
    }

    private static IServiceCollection AddIdGenServices(this IServiceCollection services, IConfiguration config)
    {
        string strMachineId = config["MACHINE_ID"]! ?? throw new ArgumentException("Enviroment variable 'MACHINE_ID' was not found");
        int machineId = int.Parse(strMachineId);

        services.AddSingleton<IIdGenerator<long>, IdGenerator>(x =>
        {
            var options = new IdGeneratorOptions()
            {
                TimeSource = new DefaultTimeSource(new DateTime(2026, 3, 1, 0, 0, 0, DateTimeKind.Utc)),
                IdStructure = new IdStructure(41, 11, 11),
                SequenceOverflowStrategy = SequenceOverflowStrategy.SpinWait,
            };
            return new IdGenerator(machineId, options);
        });
        return services;
    }

    private static IServiceCollection AddDatabaseService(this IServiceCollection services, IConfiguration config, IWebHostEnvironment enviroment)
    {
        string connString = string.Empty;

        if (enviroment.IsDevelopment() == false)
            connString = config["CONNECTION_STRING"]!;
        else
            connString = config.GetConnectionString("DefaultConnection")!;

        if(connString == null)
        {
            throw new InvalidOperationException("Connection string was not provided.");
        }


        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(connString);
        });

        services.AddScoped<IAppDbContext, AppDbContext>();
        return services;
    }


}
