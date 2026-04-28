
namespace Domain.Common.ValueObjects;

public sealed record Money
{
    public const decimal MaxValue = 1_000_000;

    internal Money()
    {
        
    }


    public static Result<Money> From(decimal value)
    {
        if(ValidationHelper.IsOutOfRange(value, 0, MaxValue))
        {
            return DomainErrors.Common.MoneyAmountInvalid;
        }

        return new Money()
        {
            Value = value
        };
    }
    public decimal Value { get; internal init; }

}
