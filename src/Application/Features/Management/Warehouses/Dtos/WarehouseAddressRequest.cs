namespace Application.Features.Management.Warehouses.Dtos;

public sealed record WarehouseAddressRequest(string FullName, string PhoneNumber, string CountryCode, string HouseNo,
        string City, string PostalCode, string AddressLine1, string? AddressLine2, string StateProvince, string? Notes)
{
    
}
