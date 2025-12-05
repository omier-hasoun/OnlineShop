


using Microsoft.AspNetCore.Identity.UI.Services;
using Presentation.Notification;

namespace Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services, IConfiguration config)
    {


        services.AddCustomServices()
                .AddHttpContextAccessor()
                .AddIdentityService()
                .AddAuthenticationService()
                .AddAuthorizatinoService();

        services.AddRazorPages();

        return services;
    }

    private static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        // for simple dependency injection Transient/Singleton/Scoped
        services.AddTransient<IEmailSender, EmailSender>();
        return services;
    }

    private static IServiceCollection AddOpenApiAndScalarService(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddOpenApiAndScalarService();
        return services;
    }

    private static IServiceCollection AddIdentityService(this IServiceCollection services)
    {
        services.AddIdentityCore<User>(options =>
        {
            // Password settings.
            options.Password.RequiredLength = UserAccountSettings.PasswordMinLength;
            options.Password.RequireDigit = UserAccountSettings.PasswordRequireDigits;
            options.Password.RequireUppercase = UserAccountSettings.PasswordRequireUppercase;
            options.Password.RequiredUniqueChars = UserAccountSettings.PasswordRequiredUniqueChars;
            options.Password.RequireNonAlphanumeric = UserAccountSettings.PasswordRequireNonAlphanumeric;
            options.Password.RequireLowercase = UserAccountSettings.PasswordRequireLowercase;

            // Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(UserAccountSettings.DefaultLockoutMinutes);
            options.Lockout.MaxFailedAccessAttempts = UserAccountSettings.MaxFailedAccessAttempts;
            options.Lockout.AllowedForNewUsers = UserAccountSettings.AllowLockOutForNewUsers;

            // User settings.
            options.User.AllowedUserNameCharacters = UserAccountSettings.AllowedUserNameChars;
            options.User.RequireUniqueEmail = UserAccountSettings.RequireUniqueEmail;

            // SignIn settings.
            options.SignIn.RequireConfirmedAccount = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;

            // Identity stores settings.
            options.Stores.MaxLengthForKeys = 128;
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
        .AddSignInManager<SignInManager<User>>()
        .AddUserManager<UserManager<User>>()
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();
        // .AddDefaultUI();

        services.AddTransient<IEmailSender<User>, EmailSenderFaker>();

        return services;
    }

    private static IServiceCollection AddAuthenticationService(this IServiceCollection services)
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
                    options.LoginPath = "/Account/Login";
                    options.LogoutPath = "/Account/Logout";
                    options.AccessDeniedPath = "/Account/AccessDenied";
                    options.SlidingExpiration = true;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);

            })
;
        return services;
    }

    private static IServiceCollection AddAuthorizatinoService(this IServiceCollection services)
    {
        services.AddAuthorization();
        return services;
    }
}
