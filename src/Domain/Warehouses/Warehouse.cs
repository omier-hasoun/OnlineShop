
namespace Domain.Warehouses;

public sealed class Warehouse : AggregateRoot<WarehouseId>
{
    private Warehouse(WarehouseId id, AddressId addressId, string name, string countryCode)
        : base(id)
    {
        AddressId = addressId;
        Name = name;
        CountryCode = countryCode;
    }
    
    public static Result<Warehouse> Create(WarehouseId id, AddressId addressId, string name, string countryCode)
    {

        return new Warehouse(id, addressId, name, countryCode);
    }
    
    public AddressId AddressId { get; private init; }

    public string Name { get; private set; } = null!;
    public string CountryCode { get; private set; } = null!;

    public Address AddressInfo { get; private set; } = null!;
} 
