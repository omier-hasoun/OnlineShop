

using System.Net;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace Presentation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            IPAddress ipAddress = IPAddress.Parse("91.35.190.39");
            builder.WebHost.ConfigureKestrel(options =>
            {
                options.Listen(ipAddress, 2501, options =>
                {
                    options.Protocols = HttpProtocols.Http2 | HttpProtocols.Http3;
                });
            });

            builder.Configuration.AddUserSecrets("7f342e59-c0e1-4ef5-9bd1-126a96fa7a5b");

            var config = builder.Configuration;

            builder.Services.AddWebApiServices(config)
                            .AddApplicationServices(config)
                            .AddInfrastructureServices(config);

            var app = builder.Build();

            app.MapIdentityApi<User>();


            if (app.Environment.IsDevelopment())
            {
                app.MapScalarApiReference();
                app.MapOpenApi();
                app.MapGet("/", () => "Hello World!");
            }
            else
            {
                // app.UseHsts();
            }



            app.Run();
        }
    }
}
