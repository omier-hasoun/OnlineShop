//using Api.Services;
using Api.Services;
using Application.Common.Identity;
using Infrastructure.BackgroundServices;
using Infrastructure.Common.Hashing;
using Infrastructure.Common.Rules;

using Microsoft.AspNetCore.Identity.UI.Services;


namespace Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration config)
    {


        services.AddCustomServices()
                .AddHttpContextAccessor()
                .AddOpenApiAndScalarServices()
                .AddIdentityServices()
                .AddAuthenticationServices()
                .AddAuthorizationServices();

        return services;
    }

    private static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        // for simple dependency injection Transient/Singleton/Scoped
        services.AddTransient<IEmailSender, EmailSenderFaker>();
        services.AddScoped<IUserContext, UserContext>();
        return services;
    }

    private static IServiceCollection AddOpenApiAndScalarServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddOpenApi();
        return services;
    }

    private static IServiceCollection AddIdentityServices(this IServiceCollection services)
    {
        services.AddIdentityCore<AppUser>(options =>
        {
            // Password settings.
            options.Password.RequiredLength = AppUserRules.MinPasswordLength;
            options.Password.RequireDigit = AppUserRules.PasswordRequireDigits;
            options.Password.RequireUppercase = AppUserRules.PasswordRequireUppercase;
            options.Password.RequiredUniqueChars = AppUserRules.PasswordRequiredUniqueChars;
            options.Password.RequireNonAlphanumeric = AppUserRules.PasswordRequireNonAlphanumeric;
            options.Password.RequireLowercase = AppUserRules.PasswordRequireLowercase;

            // Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(AppUserRules.DefaultLockoutMinutes);
            options.Lockout.MaxFailedAccessAttempts = AppUserRules.MaxFailedAccessAttempts;
            options.Lockout.AllowedForNewUsers = AppUserRules.AllowLockOutForNewUsers;

            // User settings.
            options.User.AllowedUserNameCharacters = AppUserRules.AllowedUserNameChars;
            options.User.RequireUniqueEmail = AppUserRules.RequireUniqueEmail;

            // SignIn settings.
            options.SignIn.RequireConfirmedAccount = true;
            options.SignIn.RequireConfirmedPhoneNumber = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;

            // Identity stores settings.
            options.Stores.MaxLengthForKeys = 50;
            options.Stores.ProtectPersonalData = false;
            options.Stores.SchemaVersion = IdentitySchemaVersions.Version1;

            // Claims settings.
            options.ClaimsIdentity.RoleClaimType = "role";
            // options.ClaimsIdentity.UserIdClaimType = "sub";
            options.ClaimsIdentity.UserNameClaimType = "username";
            options.ClaimsIdentity.EmailClaimType = "email";
            options.ClaimsIdentity.SecurityStampClaimType = "security_stamp";

        })
        .AddRoles<Role>()
        .AddSignInManager<SignInManager<AppUser>>()
        .AddUserManager<UserManager<AppUser>>()
        .AddEntityFrameworkStores<AppDbContext>()
        .AddApiEndpoints()
        .AddDefaultTokenProviders();

        services.AddTransient<IEmailSender<AppUser>, EmailSenderFaker>();
        services.AddScoped<IPasswordHasher<AppUser>, UserPasswordHasher>();

        return services;
    }

    private static IServiceCollection AddAuthenticationServices(this IServiceCollection services)
    {
        services.AddAuthentication(options =>
        {

            options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
            options.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
            options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
        })
            .AddCookie(IdentityConstants.ExternalScheme)
            .AddCookie(IdentityConstants.TwoFactorUserIdScheme)
            .AddCookie(IdentityConstants.ApplicationScheme, options =>
            {
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);

            })

;
        return services;
    }

    private static IServiceCollection AddAuthorizationServices(this IServiceCollection services)
    {
        services.AddAuthorization();
        return services;
    }
}
