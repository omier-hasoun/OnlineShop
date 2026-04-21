namespace Domain.Customers;

public sealed class CustomerShippingAddress : Address
{
    private CustomerShippingAddress(AddressId id, bool isDefault, UserId userId, string fullName, string phoneNumber, string countryCode, string houseNo,
        string city, string postalCode, string addressLine1, string? addressLine2 = null, string? stateProvince = null,
        decimal? longitude = null, decimal? latitude = null, string? notes = null) 
        : base(id, fullName, phoneNumber, countryCode, houseNo, city, postalCode, addressLine1, addressLine2, stateProvince, longitude, latitude, notes)
    {
        IsDefault = isDefault;
        UserId = userId;
    }

    public static Result<CustomerShippingAddress> Create(AddressId id, bool isDefault, UserId userId, string fullName, string phoneNumber, string countryCode, string houseNo,
    string city, string postalCode, string addressLine1, string? addressLine2, string? stateProvince,
    decimal? longitude, decimal? latitude, string? notes)
    {


        return new CustomerShippingAddress(id, isDefault, userId, fullName, phoneNumber, countryCode, houseNo, city, postalCode, addressLine1, addressLine2, stateProvince, longitude, latitude, notes);
    }


    public bool IsDefault { get; private set; }
    public UserId UserId { get; private init; }

    internal void UpdateIsDefault(bool isDefault)
    {
        IsDefault = isDefault;
    }

}
