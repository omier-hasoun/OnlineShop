namespace Domain.Iso;

public readonly record struct CountryId
{
    public int Value { get; init; }// auto incremented by the database
    public static implicit operator int(CountryId countryId) => countryId.Value;
    public static implicit operator CountryId(int value) => new CountryId { Value = value };

    private CountryId(int value)
    {
        Value = value;
    }
}
