using Application.Common.Identity;
using Infrastructure.Configurations;
using Infrastructure.Configurations.FileStorage;
using Scalar.AspNetCore;


namespace Api
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var config = builder.Configuration;

            if (builder.Environment.IsProduction() || builder.Environment.IsStaging())
                config.AddEnvironmentVariables();

            if (builder.Environment.IsDevelopment())
                config.AddUserSecrets("7f342e59-c0e1-4ef5-9bd1-126a96fa7a5b");

            builder.Services.Configure<FileStoragePathsOptions>(config.GetSection(nameof(FileStoragePathsOptions)));
            builder.Services.Configure<IdentityOptions>(config.GetSection(nameof(IdentityOptions)));



            builder.Services.AddApiServices(config, builder.Environment)
                            .AddApplicationServices(config)
                            .AddInfrastructureServices(config,  builder.Environment);

            var app = builder.Build();


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
            app.MapIdentityApi<AppUser>();

            app.Run();



        }
    }
}
