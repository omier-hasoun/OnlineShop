
namespace Domain.Countries;

public sealed class Country : IEntity
{
    public int Id { get; }
    public string Name { get; } = null!;
    public string Code { get; } = null!;
    public int PhoneCode { get; }
    public string? CurrencyCode { get; }
    public Country()
    {
        
    }
}
