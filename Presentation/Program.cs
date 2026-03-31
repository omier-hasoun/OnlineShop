using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Scalar.AspNetCore;


namespace Presentation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            if (builder.Environment.IsProduction())
                builder.Configuration.AddEnvironmentVariables();

            if (builder.Environment.IsDevelopment())
                builder.Configuration.AddUserSecrets("7f342e59-c0e1-4ef5-9bd1-126a96fa7a5b");

            var config = builder.Configuration;

            builder.Services.AddPresentationServices(config)
                            .AddApplicationServices(config)
                            .AddInfrastructureServices(config, builder.Environment);

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

            // Change this line:
            app.MapIdentityApi<User>();

            // To this:
            app.MapGroup("/auth")
               .MapIdentityApi<User>()
               .WithTags("Authentication");

            app.Run();
        }
    }
}
