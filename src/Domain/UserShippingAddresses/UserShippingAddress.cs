namespace Domain.UserShippingAddresses;

public sealed class UserShippingAddress : BaseEntity<UserShippingAddressId>
{
    private UserShippingAddress(UserShippingAddressId id, Guid userId, AddressId addressId, bool isDefault) : base(id)
    {
        UserId = userId;
        AddressId = addressId;
        IsDefault = isDefault;
    }

    public static Result<UserShippingAddress> Create(UserShippingAddressId id, Guid customerId, AddressId addressId, bool isDefault)
    {

        return new UserShippingAddress(id, customerId, addressId, isDefault);
    }



    public AddressId AddressId { get; private init; }
    public Guid UserId { get; private init; }
    public bool IsDefault { get; private set; }

    public Address? Address { get; }

    public void SetAsDefault()
    {
        IsDefault = true;
    }
    public void UnsetDefault()
    {
        IsDefault = false;
    }
}
