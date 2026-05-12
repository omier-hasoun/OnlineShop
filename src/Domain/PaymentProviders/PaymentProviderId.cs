
namespace Domain.PaymentProviders;

public readonly record struct PaymentProviderId
{
    public Guid Value { get; init; }

    public PaymentProviderId(Guid value)
    {
        Value = value;
    }

    public Result<Success> IsValid()
    {
        if (Value.Version != 7)
        {
            return DomainErrors.PaymentProviders.PaymentProviderIdInvalid;
        }

        return Result.Success;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
