
namespace Domain.Iso;

public sealed class Currency : BaseEntity
{
    private Currency()
    {
    }
    public static Result<Currency> Create(CurrencyId code, string name, string symbol)
    {
        return new Currency
        {
            Code = code,
            Name = name,
            Symbol = symbol
        };
    }
    public CurrencyId Code { get; private init; }
    public string Name { get; private set; } = null!;
    public string Symbol { get; private set; } = null!;
}
