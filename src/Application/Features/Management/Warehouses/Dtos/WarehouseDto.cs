using Domain.Warehouses;

namespace Application.Features.Management.Warehouses.Dtos;

public sealed record WarehouseDto
{

    public WarehouseDto(Warehouse warehouse)
    {
        Id = warehouse.Id.Value;
        Name = warehouse.Name;
        var address = warehouse.Address;
        Address = new WarehouseAddressResponse(address.Id.Value, address.FullName, address.PhoneNumber, address.CountryCode, address.HouseNo,
            address.City, address.PostalCode, address.AddressLine1, address.AddressLine2,
            address.StateProvince, address.Notes);
    }

    public long Id { get; private set; }
    public string Name { get; private set; }

    public WarehouseAddressResponse Address { get; private set; }

}
