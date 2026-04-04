
using System.ComponentModel.DataAnnotations;


namespace Domain.Common;

public sealed class DomainInvariantsException : ValidationException
{
    private DomainInvariantsException(string message)
    : base(message)
    {
        
    }
    public static void ThrowIfOutOfRange(double propertyValue, string propertyName, int minValue, int MaxValue, string? message = null)
    {
        ArgumentNullException.ThrowIfNull(propertyName);

        if (propertyValue < minValue || propertyValue > MaxValue)
        {
            message = message is null ? GenerateOutOfRangeErrorMessage(propertyValue, propertyName, minValue, MaxValue) : message;
            throw new DomainInvariantsException(message);
        }
    }

    public static string GenerateOutOfRangeErrorMessage(double propertyValue, string propertyName, int minValue, int MaxValue)
    {
        return $"The value of {propertyName} must be between {minValue} and {MaxValue}.\nValue of {propertyName} = {propertyValue}.";
    }
    public static string GenerateStringLengthOutOfRangeErrorMessage(double propertyValue, string propertyName, int minValue, int MaxValue)
    {
        return $"The value of {propertyName} must be between {minValue} and {MaxValue}.\nValue of {propertyName} = {propertyValue}.";
    }

    public static void ThrowIfStringLengthOutOfRange(string propertyValue, string propertyName, int minValue, int MaxValue, string? message = null)
    {
        ArgumentNullException.ThrowIfNull(propertyValue);

        message = message is null ? GenerateStringLengthOutOfRangeErrorMessage(propertyValue.Length, propertyName, minValue, MaxValue) : message;
        ThrowIfOutOfRange(propertyValue.Length, propertyName, minValue, MaxValue, message);
    }


}
