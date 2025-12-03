
namespace Application.Common.Abstractions;


public interface IPasswordHasher
{
    /// <summary>
    /// Hashes a plain text password using PBKDF2 with a cryptographically random salt.
    /// </summary>
    /// <param name="password">The plain text password to hash.</param>
    /// <returns>
    /// A string containing the Base64-encoded hash and Base64-encoded salt separated by a hyphen in the format: "&lt;base64-hash&gt;-&lt;base64-salt&gt;".
    /// Both the hash and the salt are encoded using Base64.
    /// </returns>
    /// <remarks>
    /// This method generates a random salt for each password hash, ensuring that identical passwords
    /// produce different hashes. The hash is computed using PBKDF2 with the configured iterations,
    /// hash algorithm, and hash size.
    /// </remarks>
    string HashPassword(string password);

    /// <summary>
    /// Verifies whether a plain-text password matches a stored password hash.
    /// </summary>
    /// <param name="providedPassword">The plain-text password to verify.</param>
    /// <param name="hashedPassword">The stored password representation expected in the format "&lt;base64-hash&gt;-&lt;base64-salt&gt;".</param>
    /// <returns>
    /// <c>true</c> if the provided plain-text password, when hashed with the extracted salt using PBKDF2
    /// (Rfc2898DeriveBytes.Pbkdf2) with the configured iterations, hash algorithm and hash size,
    /// matches the stored hash; otherwise <c>false</c>.
    /// </returns>
    /// <remarks>
    /// The method splits the stored password string on ':' into two Base64-encoded parts: the first is
    /// interpreted as the expected hash and the second as the salt. If the format is invalid or the
    /// Base64 decoding fails, the method returns <c>false</c>. Comparison is performed using a
    /// constant-time comparison (CryptographicOperations.FixedTimeEquals) to mitigate timing attacks.
    /// </remarks>
    PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword);

}
