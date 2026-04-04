
namespace Domain.Iso;

public sealed class Country : BaseEntity
{
    private Country()
    {
    }
    public static Result<Country> Create(CountryId id, CurrencyId currencyCode , string name, string isoCode, string phoneCode)
    {

        return new Country
        {
            Id = id,
            CurrencyCode = currencyCode,
            Name = name,
            PhoneCode = phoneCode,
            IsoCode = isoCode
        };
    }
    public CountryId Id { get; private init; }
    public CurrencyId CurrencyCode { get; private init; } 
    public string Name { get; private set; } = null!;
    public string IsoCode { get; private set; } = null!;
    public string PhoneCode { get; private set; } = null!;
}
