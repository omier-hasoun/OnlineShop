using System.Security.Cryptography;

namespace Infrastructure.Identity;

public sealed class AppPasswordHasher : IPasswordHasher<User>
{
    private const int _saltSize = 16;
    private const int _hashSize = 32;
    private const int _iterations = 400000;
    private readonly HashAlgorithmName _algorithm = HashAlgorithmName.SHA512;

    private string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(_saltSize);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, _iterations, _algorithm, _hashSize);

        return $"{Convert.ToBase64String(hash)}:{Convert.ToBase64String(salt)}";
    }

    public string HashPassword(User user, string password)
    {
        Console.WriteLine("Hashing password for user: " + user.UserName);
        return HashPassword(password);
    }


    public PasswordVerificationResult VerifyHashedPassword(
        User user,
        string hashedPassword,
        string providedPassword)
    {
        Console.WriteLine("Verifieng Hashed password for user: " + user.UserName);
        if (VerifyHashedPassword(providedPassword, hashedPassword) == PasswordVerificationResult.Success)
        {
            Console.WriteLine("Password verification succeeded for user: " + user.UserName);
            return PasswordVerificationResult.Success;
        }
        else
        {
            Console.WriteLine("Password verification failed for user: " + user.UserName);
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
