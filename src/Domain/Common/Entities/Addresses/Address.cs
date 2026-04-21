namespace Domain.Common.Entities.Addresses;

public class Address : BaseEntity<AddressId>
{


    protected Address(AddressId id, string fullName, string phoneNumber, string countryCode, string houseNo,
        string city, string postalCode, string addressLine1, string? addressLine2 = null, string? stateProvince = null,
        decimal? longitude = null, decimal? latitude = null, string? notes = null) : base(id)
    {
        FullName = fullName;
        PhoneNumber = phoneNumber;
        
        CountryCode = countryCode;
        StateProvince = stateProvince;
        City = city;
        PostalCode = postalCode;
        AddressLine1 = addressLine1;
        AddressLine2 = addressLine2;
        HouseNo = houseNo;
        Longitude = longitude;
        Latitude = latitude;
        Notes = notes;
    }

    public static Result<Address> Create(AddressId id, string fullName, string phoneNumber, string countryCode, string houseNo,
        string city, string postalCode, string addressLine1, string? addressLine2, string? stateProvince,
        decimal? longitude, decimal? latitude, string? notes)
    {


        return new Address(id, fullName, phoneNumber, countryCode, houseNo, city, postalCode, addressLine1, addressLine2, stateProvince, longitude, latitude, notes);
    }

    public string FullName { get; private set; } = null!;
    public string PhoneNumber { get; private set; } = null!;

    public string CountryCode { get; private set; } = null!;
    public string City { get; private set; } = null!;
    public string PostalCode { get; private set; } = null!;
    public string AddressLine1 { get; private set; } = null!;
    public string? AddressLine2 { get; private set; }
    public decimal? Longitude { get; private set; }
    public decimal? Latitude { get; private set; }
    public string? Notes { get; private set; }
    public string? StateProvince { get; private set; }
    public string HouseNo { get; private set; } = null!;


}
