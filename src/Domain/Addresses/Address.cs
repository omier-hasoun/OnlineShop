
namespace Domain.Addresses;

public sealed class Address : BaseEntity
{


    private Address(AddressId id, string fullName, string phoneNumber, string countryCode, bool isDefault,
        string city, string postalCode, string addressLine1, string? addressLine2 = null, string? stateProvince = null,
        string? companyName = null, decimal? longitude = null, decimal? latitude = null, string? notes = null)
    {
        Id = id;

        FullName = fullName;
        PhoneNumber = phoneNumber;
        
        CountryCode = countryCode;
        IsDefault = isDefault;
        StateProvince = stateProvince;
        City = city;
        PostalCode = postalCode;
        AddressLine1 = addressLine1;
        AddressLine2 = addressLine2;

        CompanyName = companyName;
        Longitude = longitude;
        Latitude = latitude;
        Notes = notes;
    }

    public static Result<Address> Create(AddressId id, string fullName, string phoneNumber, string countryCode, bool isDefault, 
        string city, string postalCode, string addressLine1, string? addressLine2 = null, string? stateProvince = null,
        string? companyName = null, decimal? longitude = null, decimal? latitude = null, string? notes = null)
    {


        return new Address(id, fullName, phoneNumber, countryCode, isDefault, city, postalCode, addressLine1, addressLine2, stateProvince, companyName, longitude, latitude, notes);
    }

    public AddressId Id { get; private init; }

    public string FullName { get; private set; } = null!;
    public string PhoneNumber { get; private set; } = null!;

    public string CountryCode { get; private set; } = null!;
    public string City { get; private set; } = null!;
    public string PostalCode { get; private set; } = null!;
    public bool IsDefault { get; private set; }
    public string AddressLine1 { get; private set; } = null!;
    public string? AddressLine2 { get; private set; }
    public decimal? Longitude { get; private set; }
    public decimal? Latitude { get; private set; }
    public string? CompanyName { get; private set; }
    public string? Notes { get; private set; }
    public string? StateProvince { get; private set; }

}
