using System;
using System.Collections.Generic;
using System.Text;
using Domain.Addresses;

namespace Domain.Warehouses;

public sealed class Warehouse
{
    private Warehouse()
    {
        
    }
    
    public static Result<Warehouse> Create(WarehouseId id, AddressId addressId, string region, string country)
    {
        return new Warehouse()
        {
            Id = id,
            AddressId = addressId,
            Region = region,
            Country = country
        };
    }
    
    public WarehouseId Id { get; private init; }
    public AddressId AddressId { get; private init; }
    public string Region { get; private set; } = null!;
    public string Country { get; private set; } = null!;

    public Address? AddressInfo { get; private set; }
} 
