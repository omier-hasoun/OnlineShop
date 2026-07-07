namespace Domain.Common.Entities.Addresses;

public class Address : BaseEntity<AddressId>
{
    private Address()
    {
        
    }

    protected Address(AddressId id, string fullName, string phoneNumber, string countryCode, string houseNo,
        string city, string postalCode, string addressLine1, string? addressLine2 = null, string? stateProvince = null,
        GeoLocation? geoLocation = null, string? notes = null) : base(id)
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
        GeoLocation = geoLocation;
        Notes = notes;
    }

    public static Result<Address> Create(AddressId id, string fullName, string phoneNumber, string countryCode, string houseNo,
        string city, string postalCode, string addressLine1, string? addressLine2, string? stateProvince,
        GeoLocation? geoLocation, string? notes)
    {

        var validationResult = Result.ValidateAll(
                                () => id.IsValid()

                               );

        if (validationResult.Failed)
            return validationResult.Errors;


        return new Address(id, fullName, phoneNumber, countryCode, houseNo, city, postalCode, addressLine1, addressLine2, stateProvince, geoLocation, notes);
    }

    public string FullName { get; private set; } = null!;
    public string PhoneNumber { get; private set; } = null!;

    public string CountryCode { get; private set; } = null!;
    public string City { get; private set; } = null!;
    public string PostalCode { get; private set; } = null!;
    public string AddressLine1 { get; private set; } = null!;
    public string? AddressLine2 { get; private set; }
    public GeoLocation? GeoLocation { get; }

    
    public string? Notes { get; private set; }
    public string? StateProvince { get; private set; }
    public string? HouseNo { get; private set; }


}
