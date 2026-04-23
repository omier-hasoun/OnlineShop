
using Domain.Customers;
using Domain.Orders;
using Domain.Products;
using IdGen;
using Infrastructure.AppIdGenerators;
using Infrastructure.AppIdGenerators.Primitives;
using Infrastructure.BackgroundServices;
using Infrastructure.Common.Abstractions;
using Infrastructure.Common.Exceptions;
using Infrastructure.Data.Interceptors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using App = Application.Common.Abstractions;


namespace Infrastructure; 

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config, IWebHostEnvironment enviroment)
    {
        services.AddCustomServices()
                .AddEfCoreServices(config, enviroment)
                .AddIdGenServices(config)
                .AddIdGeneratorsServices()



                ;

        return services;
    }

    private static IServiceCollection AddCustomServices(this IServiceCollection services)
    {

        return services;
    }

    private static IServiceCollection AddIdGeneratorsServices(this IServiceCollection services)
    {
        services.AddKeyedSingleton<IPrimitiveTypeIdGenerator<Guid>, GuidV7Generator>("GuidV7");
        services.AddKeyedSingleton<IPrimitiveTypeIdGenerator<long>, SnowflakeGenerator>("Snowflake");

        services.AddSingleton<App.IIdGenerator<UserId>, UserIdGenerator>();
        services.AddSingleton<App.IIdGenerator<ProductId>, ProductIdGenerator>();
        services.AddSingleton<App.IIdGenerator<OrderId>, OrderIdGenerator>();


        return services;
    }

    private static IServiceCollection AddIdGenServices(this IServiceCollection services, IConfiguration config)
    {

        string strMachineId = config["MACHINE_ID"]! ?? throw new MachineIdWasNotProvidedException();
        int machineId = int.Parse(strMachineId);

        services.AddSingleton<IdGen.IIdGenerator<long>, IdGen.IdGenerator>((p) =>
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

    private static IServiceCollection AddEfCoreServices(this IServiceCollection services, IConfiguration config, IWebHostEnvironment environment)
    {
        string? connString;

        bool isProductionOrStaging = environment.IsDevelopment() == false;
        if (isProductionOrStaging)
            connString = config["CONNECTION_STRING"];
        else
            connString = config.GetConnectionString("DefaultConnection")!;

        if (connString == null)
        {
            throw new ConnectionStringWasNotProvidedException();
        }


        services.AddDbContext<AppDbContext>((sp, options) =>
        {
            options.UseSqlServer(connString).AddInterceptors(
            sp.GetRequiredService<SoftDeleteEntitySaveChangesInterceptor>(),
            sp.GetRequiredService<AuditedEntitySaveChangesInterceptor>());
        });

        services.AddScoped<IAppDbContext, AppDbContext>();

        //interceptors
        services.AddScoped<AuditedEntitySaveChangesInterceptor>();
        services.AddScoped<SoftDeleteEntitySaveChangesInterceptor>();

        return services;
    }


}
