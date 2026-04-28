
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
    public static PaymentProviderId Parse(string value)
    {
        if (TryParse(value, out var id))
            return id;
        throw new ArgumentException("PaymentProviderId is invalid.", nameof(value));
    }
    public static bool TryParse(string value, out PaymentProviderId id)
    {
        if (Guid.TryParse(value, out var brandId))
        {
            id = new PaymentProviderId(brandId);
            return true;
        }
        id = new();
        return false;
    }
}
