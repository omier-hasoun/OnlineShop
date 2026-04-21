


namespace Domain.Customers;

public sealed class Customer : AggregateRoot<UserId>, ISoftDeleted
{
    private Customer(UserId Id, bool isDeleted) : base(Id)
    {
        IsDeleted = isDeleted;
    }

    public static Result<Customer> Create(UserId Id)
    {


        return new Customer(Id, isDeleted: false);
    }
    public UserId UserId { get {  return Id; }  }

    public bool IsDeleted { get; private set; }


    private List<CustomerShippingAddress> _shippingAddresses = [];
    public IReadOnlyCollection<CustomerShippingAddress> ShippingAddresses { get { return _shippingAddresses.AsReadOnly(); } private set { _shippingAddresses = value.ToList(); } }


    private List<CartItem> _cartItems = [];
    public IReadOnlyCollection<CartItem> CartItems { get { return _cartItems.AsReadOnly(); } private set { _cartItems = value.ToList(); } }


    public Result<AddressId> AddNewShippingAddress(AddressId id, bool isDefault, UserId userId, string fullName, string phoneNumber, string countryCode, string houseNo,
        string city, string postalCode, string addressLine1, string? addressLine2, string? stateProvince,
        decimal? longitude, decimal? latitude, string? notes)
    {
        if (_shippingAddresses.Count >= CustomerRules.MaxAddressesPerCustomer)
        {
            return Error.Validation("MaxReached");
        }

        var createAddressResult = CustomerShippingAddress.Create(id, isDefault, userId, fullName, phoneNumber, countryCode, houseNo, city, postalCode, addressLine1, addressLine2, stateProvince, longitude, latitude, notes);

        if (createAddressResult.Failed)
        {
            return createAddressResult.Errors;
        }
        var address = createAddressResult.Value;

        if (address.IsDefault)
        {
            SetAllShippingAddressesToNonDefault();
        }

        _shippingAddresses.Add(address);

        return address.Id;
    }

    public Result<Success> SetAsDefaultShippingAddress(AddressId addressId)
    {
        SetAllShippingAddressesToNonDefault();

        var newDefaultAddress = _shippingAddresses.FirstOrDefault(x => x.Id == addressId);

        if (newDefaultAddress is null)
        {
            return Error.NotFound("");//should change
        }

        newDefaultAddress.UpdateIsDefault(true);

        return Result.Success;
    }

    public Result<Success> RemoveShippingAddress(AddressId addressId)
    {
        var address = _shippingAddresses.FirstOrDefault(y => y.Id == addressId);

        if (address is null)
        {
            return Error.NotFound("");//should change

        }

        _shippingAddresses.Remove(address);
        return Result.Success;
    }

    public void SetAllShippingAddressesToNonDefault()
    {
        _shippingAddresses.ForEach(address => address.UpdateIsDefault(false));
    }
}
