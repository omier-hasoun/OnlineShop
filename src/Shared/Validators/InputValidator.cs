namespace Shared.Validators;

public static class DataValidator
{
    public static bool IsNullOrContainsWhiteSpace(string str)
    {
        return !(str is null || str.Contains(' '));
    }

    public static bool IsValidAge(DateTime date, int TargetAge)
    {
        DateOnly dateOnly = DateOnly.FromDateTime(date);
        return dateOnly.AddYears(TargetAge) >= DateOnly.FromDateTime(DateTime.UtcNow);
    }

    public static bool IsValidLength(string str, int min, int max)
    {
        return str.Length >= min && str.Length <= max;
    }
}
