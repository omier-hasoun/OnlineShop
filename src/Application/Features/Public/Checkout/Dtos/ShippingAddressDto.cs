
namespace Application.Features.Public.Checkout.Dtos;

public sealed record ShippingAddressDto(
string? FullName, string? PhoneNumber, string Country, string? HouseNo,
string City, string PostalCode, string AddressLine1, string? AddressLine2 = null, string? StateProvince = null
);
