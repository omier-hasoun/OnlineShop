using System.Security.Cryptography;

namespace Infrastructure.LocalServices.HashingService;

public sealed class UserPasswordHasher : IPasswordHasher<AppUser>
{
    private const int _saltSize = 16;
    private const int _hashSize = 32;
    private const int _iterations = 120000;
    private readonly HashAlgorithmName _algorithm = HashAlgorithmName.SHA256;

    private string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(_saltSize);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, _iterations, _algorithm, _hashSize);

        return $"{Convert.ToBase64String(hash)}:{Convert.ToBase64String(salt)}";
    }

    public string HashPassword(AppUser user, string password)
    {
        return HashPassword(password);

    }


    public PasswordVerificationResult VerifyHashedPassword(AppUser user, string hashedPassword, string providedPassword)
    {
        if (VerifyHashedPassword(providedPassword, hashedPassword) == PasswordVerificationResult.Success)
        {
            return PasswordVerificationResult.Success;
        }
        else
        {
            return PasswordVerificationResult.Failed;
        }
    }

    // Application-layer API
    private PasswordVerificationResult VerifyHashedPassword(string providedPassword, string hashedPassword)
    {
        string[] parts = hashedPassword.Split(':');
        if (parts.Length != 2) return PasswordVerificationResult.Failed;

        Span<byte> buffer = stackalloc byte[_hashSize];
        if (!Convert.TryFromBase64String(parts[0], buffer, out int _) ||
            !Convert.TryFromBase64String(parts[1], buffer, out int _))
            return PasswordVerificationResult.Failed;

        byte[] hash = Convert.FromBase64String(parts[0]);
        byte[] salt = Convert.FromBase64String(parts[1]);

        byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(providedPassword, salt, _iterations, _algorithm, _hashSize);

        return CryptographicOperations.FixedTimeEquals(hash, inputHash)
            ? PasswordVerificationResult.Success
            : PasswordVerificationResult.Failed;
    }
}
