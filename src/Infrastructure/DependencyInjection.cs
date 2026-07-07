
using System.Threading.Channels;
using Application.Common.Configurations;
using Application.Entities;
using Domain.Carts;
using Domain.Carts.CartItems;
using Domain.Common.Entities.Addresses;
using Domain.Orders;
using Domain.Orders.OrderLines;
using Domain.ProductGroups;
using Domain.ProductGroups.Products;
using Domain.Warehouses;
using FileSignatures;
using Infrastructure.BackgroundJobs;
using Infrastructure.Channels;
using Infrastructure.Common.Exceptions;
using Infrastructure.Data.IdGenerators;
using Infrastructure.Data.IdGenerators.Primitives;
using Infrastructure.Data.Interceptors;
using Infrastructure.ExternalServices.Email.Console;
using Infrastructure.ExternalServices.Payment.StripeService;
using Infrastructure.LocalServices.DiscountReset;
using Infrastructure.LocalServices.FileNameGeneratorService;
using Infrastructure.LocalServices.FileStorageService;
using Infrastructure.LocalServices.FileValidator;
using Infrastructure.LocalServices.Hashing;
using Infrastructure.LocalServices.ImagesStore;
using Infrastructure.LocalServices.Messaging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
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
                .AddIdGeneratorServices()
                .AddIdentityServices()
                .AddFileSignaturesServices()
                .AddOptions(config);
        return services;
    }


    private static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<ProductImagePathOptions>(config.GetSection(nameof(ProductImagePathOptions)));

        services.Configure<StripeOptions>(options =>
        {
            options.TestKey = config["STRIPE_TEST_KEY"] ?? throw new StripeApiKeyWasNotProvidedException();
        });

        services.Configure<FormOptions>(options =>
        {
            options.MultipartBodyLengthLimit = 20 * 1024 * 1024;//20 mb
            options.MemoryBufferThreshold = 32 * 1024; // 32 KB
        });

        return services;
    }

    private static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        services.AddTransient<IImageValidator, ImageValidator>();
        services.AddSingleton<IFileStorageService, LocalFileStorageService>();

        services.AddHostedService<ImageProcessingWorker>();
        services.AddHostedService<ZeroOclockWorker>();
        services.AddHostedService<StripeEventProcessingWorker>();
        services.AddHostedService<OrderRefundProcessingWorker>();
        services.AddHostedService<OutboxMessagesProcessingWorker>();


        services.AddSingleton<ApplicationSettings>(sp =>
        {
            var baseUrl = sp.GetRequiredService<IConfiguration>()["BaseUrl"];
            var businessName = sp.GetRequiredService<IConfiguration>()["BusinessName"];

            return new ApplicationSettings(baseUrl, businessName);
        });

        services.AddSingleton(sp => {

            return Channel.CreateBounded<ImageProcessingJob>(new BoundedChannelOptions(100)
            {
                FullMode = BoundedChannelFullMode.Wait,
                SingleWriter = false,
                SingleReader = true,
                AllowSynchronousContinuations = false,
            });
            
         });
        services.AddSingleton<IImageStorageService, ImagesStoreService>();
        services.AddSingleton<IImageJobReader, ImageProcessingJobsChannel>();
        services.AddSingleton<IImageJobWriter, ImageProcessingJobsChannel>();
        services.AddSingleton<IPaymentGateway, StripePaymentGateway>();
        services.AddSingleton<IUniqueFileNameGenerator, FileNameGenerator>();
        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

        services.AddSingleton<IZeroOclockService, DiscountResetService>();
        services.AddSingleton<INotificationService, ConsoleMailService>();


        return services;
    }

    private static IServiceCollection AddIdGeneratorServices(this IServiceCollection services)
    {
        services.AddKeyedSingleton<IPrimitiveTypeIdGenerator<Guid>, GuidV7Generator>("GuidV7");
        services.AddKeyedSingleton<IPrimitiveTypeIdGenerator<long>, SnowflakeGenerator>("Snowflake");

        services.AddSingleton<App.IIdGenerator<ProductGroupId>, ProductGroupIdGenerator>();

        services.AddSingleton<App.IIdGenerator<ProductId>, ProductIdGenerator>();

        services.AddSingleton<App.IIdGenerator<OrderLineId>, OrderLineIdGenerator>();

        services.AddSingleton<App.IIdGenerator<OrderId>, OrderIdGenerator>();
        services.AddSingleton<App.IIdGenerator<CartId>, CartIdGenerator>();
        services.AddSingleton<App.IIdGenerator<CartItemId>, CartItemIdGenerator>();
        services.AddSingleton<App.IIdGenerator<WarehouseId>, WarehouseIdGenerator>();
        services.AddSingleton<App.IIdGenerator<AddressId>, AddressIdGenerator>();
        
        return services;
    }

    private static IServiceCollection AddIdGenServices(this IServiceCollection services, IConfiguration config)
    {

        string strMachineId = config["MACHINE_ID"] ?? throw new MachineIdWasNotProvidedException();

        if(!byte.TryParse(strMachineId, out var machineId))
        {
            throw new MachineIdWasNotProvidedException();
        }

        services.AddSingleton<IdGen.IIdGenerator<long>, IdGen.IdGenerator>((p) =>
        {
            var options = new IdGen.IdGeneratorOptions()
            {
                TimeSource = new IdGen.DefaultTimeSource(new DateTime(2026, 5, 25, 0, 0, 0, DateTimeKind.Utc)),
                IdStructure = new IdGen.IdStructure(41, 11, 11),
                SequenceOverflowStrategy = IdGen.SequenceOverflowStrategy.SpinWait,
            };
            return new IdGen.IdGenerator(machineId, options);
        });



        return services;
    }

    private static IServiceCollection AddIdentityServices(this IServiceCollection services)
    {

        services.AddIdentityCore<AppUser>()
                    .AddRoles<Role>()
                    .AddEntityFrameworkStores<AppDbContext>()
                    .AddApiEndpoints()
                    .AddDefaultTokenProviders();

        services.AddTransient<IEmailSender<AppUser>, EmailSenderFaker>();
        services.AddScoped<IPasswordHasher<AppUser>, UserPasswordHashService>();

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
    private static string GetDbConnectionString(IConfiguration config, IWebHostEnvironment environment)
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
        return connString;
    }


    private static IServiceCollection AddEfCoreServices(this IServiceCollection services, IConfiguration config, IWebHostEnvironment environment)
    {
        services.AddDbContext<AppDbContext>((sp, options) =>
        {
            options.UseSqlServer(GetDbConnectionString(config, environment)).AddInterceptors(
                sp.GetRequiredService<SoftDeleteEntitySaveChangesInterceptor>(),
                sp.GetRequiredService<AuditedEntitySaveChangesInterceptor>(),
                sp.GetRequiredService<EventsPublisherSaveChangesInterceptor>()
            );
        });

        services.AddScoped<IAppDbContext, AppDbContext>();

        //interceptors
        services.AddScoped<AuditedEntitySaveChangesInterceptor>();
        services.AddScoped<EventsPublisherSaveChangesInterceptor>();

        services.AddScoped<SoftDeleteEntitySaveChangesInterceptor>();

        return services;
    }


}
