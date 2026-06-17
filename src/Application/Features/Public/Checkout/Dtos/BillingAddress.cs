
namespace Application.Features.Public.Checkout.Dtos;

public sealed record BillingAddress(
string FullName, string PhoneNumber, string CountryCode, string HouseNo,
string City, string PostalCode, string AddressLine1, string? AddressLine2 = null, string? StateProvince = null
);
