
using System.Threading.Channels;
using Application.Common.InternalModels;
using Domain.Customers;
using Domain.Orders;
using Domain.Products;
using Domain.Products.ProductVariants;
using FileSignatures;
using IdGen;
using Infrastructure.Channels;
using Infrastructure.Common.Abstractions;
using Infrastructure.Common.Exceptions;
using Infrastructure.Data.IdGenerators;
using Infrastructure.Data.IdGenerators.Primitives;
using Infrastructure.Data.Interceptors;
using Infrastructure.LocalServices.BackgroundServices;
using Infrastructure.LocalServices.FileNameGeneratorService;
using Infrastructure.LocalServices.FileStorageService;
using Infrastructure.LocalServices.FileValidationService;
using Infrastructure.LocalServices.HashingService;
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
                .AddIdentityServices(config, env: enviroment)
                .AddFileSignaturesServices()
                ;

        return services;
    }

    private static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        services.AddSingleton<IFileSignetureValidator, FileValidator>();
        services.AddSingleton<IFileStorageService, LocalFileStorage>();
        services.AddHostedService<ImagesProcessorWorker>();

        services.AddSingleton<Channel<ImageProcessingTask>>(sp => {

            return Channel.CreateBounded<ImageProcessingTask>(new BoundedChannelOptions(100)
            {
                FullMode = BoundedChannelFullMode.Wait,
                SingleWriter = false,
                SingleReader = true,
                AllowSynchronousContinuations = false,
            });
            
         });
        services.AddSingleton<IImageProcessingService, ProcessingImagesTasksChannel>();
        services.AddSingleton<IImageTaskReader, ProcessingImagesTasksChannel>();
        services.AddSingleton<IUniqueFileNameGenerator, FileNameGenerator>();


        return services;
    }

    private static IServiceCollection AddIdGeneratorsServices(this IServiceCollection services)
    {
        services.AddKeyedSingleton<IPrimitiveTypeIdGenerator<Guid>, GuidV7Generator>("GuidV7");
        services.AddKeyedSingleton<IPrimitiveTypeIdGenerator<long>, SnowflakeGenerator>("Snowflake");

        services.AddSingleton<App.IIdGenerator<CustomerId>, UserIdGenerator>();
        services.AddSingleton<App.IIdGenerator<ProductId>, ProductIdGenerator>();

        services.AddSingleton<App.IIdGenerator<ProductVariantId>, ProductVariantIdGenerator>();

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
    private static void GetIdentityOptions(IdentityOptions options, IConfiguration config)
    {
        var identitySection = config.GetSection("IdentityOptions");

        identitySection.GetSection("Lockout").Bind(options.Lockout);
        identitySection.GetSection("Password").Bind(options.Password);
        identitySection.GetSection("SignIn").Bind(options.SignIn);
        identitySection.GetSection("User").Bind(options.User);
        identitySection.GetSection("ClaimsIdentity").Bind(options.ClaimsIdentity);
    }

    private static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config, IWebHostEnvironment env)
    {




        services.AddIdentityCore<AppUser>((options) => GetIdentityOptions(options, config))
        .AddRoles<Role>()
        .AddEntityFrameworkStores<AppDbContext>()
        .AddApiEndpoints()
        .AddDefaultTokenProviders();

        services.AddTransient<IEmailSender<AppUser>, EmailSenderFaker>();
        services.AddScoped<IPasswordHasher<AppUser>, UserPasswordHasher>();

        return services;
    }
    private static IServiceCollection AddFileSignaturesServices(this IServiceCollection services)
    {
        services.AddSingleton<IFileFormatInspector, FileFormatInspector>( sp =>
        {
            return new FileFormatInspector(FileFormatLocator.GetFormats());
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
