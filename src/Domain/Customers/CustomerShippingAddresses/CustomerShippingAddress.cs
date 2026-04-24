namespace Domain.Customers.CustomerShippingAddresses;

public sealed class CustomerShippingAddress : BaseEntity<CustomerShippingAddressId>
{
    private CustomerShippingAddress(CustomerShippingAddressId id, UserId customerId, AddressId addressId, bool isDefault) : base(id)
    {
        CustomerId = customerId;
        AddressId = addressId;
        IsDefault = isDefault;
    }

    public static Result<CustomerShippingAddress> Create(CustomerShippingAddressId id, UserId customerId, AddressId addressId, bool isDefault)
    {

        return new CustomerShippingAddress(id, customerId, addressId, isDefault);
    }



    public AddressId AddressId { get; private init; }
    public UserId CustomerId { get; private init; }
    public bool IsDefault { get; private set; }

    public Address? Address { get; }

    internal void SetAsDefault()
    {
        IsDefault = true;
    }
    internal void UnsetDefault()
    {
        IsDefault = false;
    }
}
