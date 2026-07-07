namespace Shared.Helpers;

public static class ValHelper
{
    public static bool IsNullOrContainsWhiteSpace(string str)
    {
        return !(str is null || str.Contains(' '));
    }

    public static bool IsValidAge(DateTime date, int TargetAgeInYears)
    {
        DateOnly dateOnly = DateOnly.FromDateTime(date);
        return dateOnly.AddYears(TargetAgeInYears) >= DateOnly.FromDateTime(DateTime.UtcNow);
    }

    public static bool IsValidTextLength(string str, int min, int max)
    {
        return str.Length >= min && str.Length <= max;
    }

    public static bool IsDateInFuture(DateOnly? dateUtc)
    {
        if (!dateUtc.HasValue)
        {
            return false;
        }

        return DateOnly.FromDateTime(DateTime.UtcNow) < dateUtc;
    }

    public static bool IsOutOfRange(decimal value, decimal min, decimal max)
    {
        return value < min || value > max;
    }
    public static bool IsOutOfRange(float value, float min, float max)
    {
        return value < min || value > max;
    }
    public static bool IsOutOfRange(int value, int min, int max)
    {
        return value < min || value > max;
    }
    public static bool IsOutOfRange(long value, long min, long max)
    {
        return value < min || value > max;
    }
}
