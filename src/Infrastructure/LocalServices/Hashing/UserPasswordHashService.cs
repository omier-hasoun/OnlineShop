using System.Security.Cryptography;
using Application.Entities;

namespace Infrastructure.LocalServices.Hashing;

public sealed class UserPasswordHashService : IPasswordHasher<AppUser>
{
    private const int _saltSize = 16;
    private const int _hashSize = 32;
    private const int _iterations = 120000;
    private readonly HashAlgorithmName _algorithm = HashAlgorithmName.SHA256;

    public string HashPassword(AppUser user, string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(_saltSize);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, _iterations, _algorithm, _hashSize);

        return $"{Convert.ToBase64String(hash)}:{Convert.ToBase64String(salt)}";

    }

    public PasswordVerificationResult VerifyHashedPassword(AppUser user, string hashedPassword, string providedPassword)
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
