
namespace Domain.PaymentProviders;

public readonly record struct PaymentProviderId
{
    public Guid Value { get; init; }

    public static implicit operator Guid(PaymentProviderId paymentProviderId) => paymentProviderId.Value;
    public static implicit operator PaymentProviderId(Guid value) => new PaymentProviderId(value);
    public PaymentProviderId(Guid value)
    {
        if (value.Version != 7 || value == default)
            throw new ArgumentException("PaymentProviderId is invalid.", nameof(value));

        Value = value;
    }
}
