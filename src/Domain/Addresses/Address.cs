using Domain.Common.Enums;

namespace Domain.Addresses;

public class Address : BaseEntity
{


    protected Address()
    {

    }

    public static Result<Address> Create(AddressId id, string fullName, string phoneNumber, CountryCode countryCode, string city, string postalCode, string addressLine1, string? addressLine2 = null, string? stateProvince = null, string? companyName = null, decimal? longitude = null, decimal? latitude = null)
    {
        return new Address()
        {
            Id = id,
            FullName = fullName,
            PhoneNumber = phoneNumber,
            StateProvince = stateProvince,
            CompanyName = companyName,
            Longitude = longitude,
            Latitude = latitude,
            CountryCode = countryCode,
            City = city,
            AddressLine1 = addressLine1,
            AddressLine2 = addressLine2,
            PostalCode = postalCode,
        };
    }

    public AddressId Id { get; protected init; }
    public string FullName { get; protected set; } = null!;
    public string PhoneNumber { get; protected set; } = null!;

    public CountryCode CountryCode { get; protected set; }
    public string City { get; protected set; } = null!;
    public string PostalCode { get; protected set; } = null!;
    public string? StateProvince { get; protected set; }
    public string AddressLine1 { get; protected set; } = null!;
    public string? AddressLine2 { get; protected set; }
    public decimal? Longitude { get; protected set; }
    public decimal? Latitude { get; protected set; }
    public string? CompanyName { get; protected set; }




}
