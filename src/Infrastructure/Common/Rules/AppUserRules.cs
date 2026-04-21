namespace Infrastructure.Common.Rules;

public static class AppUserRules
{
    // Username rules
    public const int MinUserNameLength = 5;
    public const int MaxUserNameLength = 254;
    public const bool RequireUniqueUserName = true;
    public const string AllowedUserNameChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_@.-";
    public const bool RequireUniqueEmail = true;

    // Password rules
    public const int MinPasswordLength = 4;
    public const int MaxPasswordLength = 64;
    public const bool PasswordRequireDigits = false;
    public const bool PasswordRequireUppercase = false;
    public const bool PasswordRequireLowercase = false;
    public const bool PasswordRequireNonAlphanumeric = false; // special characters
    public const int PasswordRequiredUniqueChars = 3; // number of distinct characters


    // lockout / security rules
    public const int MaxFailedAccessAttempts = 3;
    public const int DefaultLockoutMinutes = 5;
    public const bool AllowLockOutForNewUsers = true;
}
