using Application.Common.AppSettingsConfiguration.FileStoragePaths;
using Application.Common.AppSettingsConfiguration.FileStoragePaths.ProductsPaths;
using Application.Common.Identity;
using Microsoft.Extensions.Options;
using Scalar.AspNetCore;


namespace Api
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var config = builder.Configuration;

            if (builder.Environment.IsProduction() || builder.Environment.IsStaging())
                config.AddEnvironmentVariables();

            if (builder.Environment.IsDevelopment())
                config.AddUserSecrets("7f342e59-c0e1-4ef5-9bd1-126a96fa7a5b");

            builder.Services.Configure<ProductPathsOptions>(config.GetSection(nameof(ProductPathsOptions)));
            builder.Services.Configure<FileStoragePathsOptions>(config.GetSection(nameof(FileStoragePathsOptions)));

            builder.Services.Configure<IdentityOptions>(config.GetSection(nameof(IdentityOptions)));



            builder.Services.AddApiServices(config, builder.Environment)
                            .AddApplicationServices(config)
                            .AddInfrastructureServices(config,  builder.Environment);

            var app = builder.Build();

            NetVips.Cache.MaxFiles = 0;
            NetVips.Cache.MaxMem = 0;
            NetVips.NetVips.Concurrency = 2;

            if (app.Environment.IsDevelopment())
            {
                app.MapScalarApiReference();
                app.MapOpenApi();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.MapGroup("/api/auth").
                MapIdentityApi<AppUser>();

            using (var scope = app.Services.CreateScope())
            {
                ApplicationDbContextInitialiser initialiser = new(scope.ServiceProvider.GetRequiredService<ILogger<ApplicationDbContextInitialiser>>(),
                    scope.ServiceProvider.GetRequiredService<AppDbContext>(),
                    scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>(),
                    scope.ServiceProvider.GetRequiredService<RoleManager<Role>>());

                await initialiser.InitialiseAndSeedData();
                IOptions<ProductPathsOptions> options = scope.ServiceProvider.GetRequiredService<IOptions<ProductPathsOptions>>();

            }
            app.Run();



        }
    }
}
