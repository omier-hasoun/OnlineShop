
namespace Application.Common;

public class UserAccountSettings
{
    // Username rules
    public const int UserNameMinLength = 4;
    public const int UserNameMaxLength = 20;
    public const bool UserNameRequireUnique = true;
    public const string AllowedUserNameChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_@.-";
    public const bool RequireUniqueEmail = true;

    // Password rules
    public const int PasswordMinLength = 4;
    public const int PasswordMaxLength = 64;
    public const bool PasswordRequireDigits = false;
    public const bool PasswordRequireUppercase = false;
    public const bool PasswordRequireLowercase = false;
    public const bool PasswordRequireNonAlphanumeric = false; // special characters
    public const int PasswordRequiredUniqueChars = 3; // number of distinct characters


    // Optional lockout / security settings
    public const int MaxFailedAccessAttempts = 3;
    public const int DefaultLockoutMinutes = 5;
    public const bool AllowLockOutForNewUsers = true;

}
