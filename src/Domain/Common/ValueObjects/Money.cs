
namespace Domain.Common.ValueObjects;

public sealed record Money
{
    public const long MaxValue = 10_000_000_000;
    public Money(decimal value)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(value, 0, nameof(value));

        ArgumentOutOfRangeException.ThrowIfGreaterThan(value, MaxValue, nameof(value));
        Value = value;
    }

    public decimal Value { get; }
}
