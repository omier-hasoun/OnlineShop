
namespace Domain.Iso;


public readonly record struct CurrencyId
{
    public string Value { get; init; }// auto incremented by the database
    public static implicit operator string(CurrencyId currencyCode) => currencyCode.Value;
    public static implicit operator CurrencyId(string value) => new CurrencyId { Value = value };

    public CurrencyId(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) 
            throw new ArgumentNullException(nameof(value));

        if(value.Length != 3) 
            throw new ArgumentException("Currency code must be 3 characters long.", nameof(value));

        Value = value;
    }
}
