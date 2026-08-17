

using System.Collections.Concurrent;
using System.Security.Cryptography;
using Application.Entities;

namespace Infrastructure.Services.Users;


internal sealed class UserResetPasswordTokenProvider : IUserTwoFactorTokenProvider<AppUser>
{
    private static readonly ConcurrentDictionary<string, ResetCodeEntry> _codes = new();

    public Task<bool> CanGenerateTwoFactorTokenAsync(
        UserManager<AppUser> manager,
        AppUser user)
    {
        return Task.FromResult(true);
    }

    public Task<string> GenerateAsync(
        string purpose,
        UserManager<AppUser> manager,
        AppUser user)
    {
        var code = RandomNumberGenerator
            .GetInt32(1000, 9999)
            .ToString();

        var key = CreateKey(user.Id, purpose);

        _codes[key] = new ResetCodeEntry
        {
            Code = code,
            ExpiresAt = DateTime.UtcNow.AddMinutes(5)
        };

        return Task.FromResult(code);
    }

    public Task<bool> ValidateAsync(
        string purpose,
        string token,
        UserManager<AppUser> manager,
        AppUser user)
    {
        var key = CreateKey(user.Id, purpose);

        if (!_codes.TryGetValue(key, out var entry))
        {
            return Task.FromResult(false);
        }

        if (entry.ExpiresAt < DateTime.UtcNow)
        {
            _codes.TryRemove(key, out _);
            return Task.FromResult(false);
        }

        var valid = entry.Code == token;

        if (valid)
        {
            // Prevent reuse
            _codes.TryRemove(key, out _);
        }

        return Task.FromResult(valid);
    }

    private static string CreateKey(Guid userId, string purpose)
    {
        return $"{userId}:{purpose}";
    }

    private sealed class ResetCodeEntry
    {
        public required string Code { get; init; }
        public required DateTime ExpiresAt { get; init; }
    }
}
