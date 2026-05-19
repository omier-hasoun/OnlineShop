
namespace Domain.Warehouses;

public sealed class Warehouse : AggregateRoot<WarehouseId>
{
    private Warehouse(WarehouseId id, AddressId addressId, string name)
        : base(id)
    {
        AddressId = addressId;
        Name = name;
    }
    
    public static Result<Warehouse> Create(WarehouseId id, AddressId addressId, string name)
    {

        return new Warehouse(id, addressId, name);
    }
    
    public AddressId AddressId { get; private init; }

    public Address Address { get; set; }

    public string Name { get; private set; } = null!;
} 
