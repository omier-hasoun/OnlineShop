
namespace Domain.Common.ValueObjects;

public sealed record Money
{
    public const int MaxValue = 1_000_000;
    public static readonly Money Zero =new Money() { Value = 0};

    internal Money()
    {
        
    }


    public static Money Create(decimal value)
    {
        if(ValHelper.IsOutOfRange(value, 0, MaxValue))
        {
            throw new ArgumentException("money cannot be less than zero");
        }

        return new Money()
        {
            Value = value
        };
    }

    public long ToCents()
    {
        return (long)Math.Round(
            Value * 100m,
            0,
            MidpointRounding.AwayFromZero
        );
    }

    public decimal Value { get; internal init; }
    public static bool operator <(Money left, Money right) =>
        left is not null && right is not null && left.Value < right.Value;

    public static bool operator >(Money left, Money right) =>
        left is not null && right is not null && left.Value > right.Value;

    public static bool operator <=(Money left, Money right) =>
        left is not null && right is not null && left.Value <= right.Value;

    public static bool operator >=(Money left, Money right) =>
        left is not null && right is not null && left.Value >= right.Value;

    public static Money operator *(Money left, Money right)
    {
        if(left is null || right is null)
        {
            return Create(0);
        }


      return Create(left.Value * right.Value);
    }

    public static Money operator +(Money left, Money right)
    {
        if (left is null || right is null)
        {
            throw new ArgumentNullException();
        }


        return Create(left.Value + right.Value);
    }
}
