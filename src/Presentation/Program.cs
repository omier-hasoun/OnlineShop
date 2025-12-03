

using System.Net;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace Presentation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddUserSecrets("7f342e59-c0e1-4ef5-9bd1-126a96fa7a5b");

            var config = builder.Configuration;

            builder.Services.AddPresentation(config)
                            .AddApplicationServices(config)
                            .AddInfrastructureServices(config);

            var app = builder.Build();

            app.MapIdentityApi<User>();


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
            app.UseAuthorization();
            app.MapRazorPages();

            app.Run();
        }
    }
}
