
using Application.Common.Configurations;
using Application.Entities;
using Microsoft.AspNetCore.Http.Features;
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

            if (!builder.Environment.IsDevelopment())
                config.AddEnvironmentVariables();
            else 
                config.AddUserSecrets("7f342e59-c0e1-4ef5-9bd1-126a96fa7a5b");

            builder.Services.Configure<ProductImagePathOptions>(config.GetSection(nameof(ProductImagePathOptions)));

            builder.Services.Configure<IdentityOptions>(config.GetSection(nameof(IdentityOptions)));

            builder.Services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 20 * 1024 * 1024;//20 mb
                options.MemoryBufferThreshold = 32 * 1024; // 32 KB
            });

            builder.Services.AddApiServices(config, builder.Environment)
                            .AddApplicationServices(config)
                            .AddInfrastructureServices(config,  builder.Environment);




            GlobalSetups.Init();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapScalarApiReference();
                app.MapOpenApi();
            }
            else
            {
                app.UseExceptionHandler("/error");
                app.UseStatusCodePages();
                //app.UseHsts();
            }

            app.UseHttpsRedirection();



            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();



            using (var scope = app.Services.CreateScope())
            {
                ApplicationDbContextInitialiser initialiser = new(scope.ServiceProvider.GetRequiredService<ILogger<ApplicationDbContextInitialiser>>(),
                    scope.ServiceProvider.GetRequiredService<AppDbContext>(),
                    scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>(),
                    scope.ServiceProvider.GetRequiredService<RoleManager<Role>>());

                await initialiser.InitialiseAndSeedData();
                IOptions<ProductImagePathOptions> options = scope.ServiceProvider.GetRequiredService<IOptions<ProductImagePathOptions>>();
            }

            app.Use(async (HttpContext context, RequestDelegate next) =>
            {
                if(!Guid.TryParse(context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var _))
                {
                    var guestId = context.Request.Cookies["guest_id"];

                    if (!Guid.TryParse(guestId, out var _))
                    {
                        guestId = Guid.CreateVersion7().ToString();

                        context.Response.Cookies.Append("guest_id", guestId,
                        new CookieOptions
                        {
                            IsEssential = true,
                            HttpOnly = true,
                            SameSite = SameSiteMode.Strict,
                            Expires = DateTimeOffset.Now.AddDays(7),
                            Secure = true,
                        });
                        context.Items["guest_id"] = guestId;
                    }

                }
                await next(context);

            });

            app.MapControllers();

            app.MapGroup("/api/auth").
                MapIdentityApi<AppUser>();

            await app.RunAsync();



        }
    }
}
