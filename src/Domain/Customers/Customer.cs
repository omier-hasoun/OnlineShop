
using Domain.Customers.CustomerShippingAddresses;

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


    private Result<Success> AddShippingAddress(CustomerShippingAddressId shippingAddressId, AddressId addressId, bool isDefault)
    {
        if (_shippingAddresses.Count >= CustomerRules.MaxAddressesPerCustomer)
        {
            return Error.Validation("MaxReached");
        }

        var createAddressResult = CustomerShippingAddress.Create(shippingAddressId, this.Id, addressId, isDefault);

        if (createAddressResult.Failed)
        {
            return createAddressResult.Errors;
        }
        var shippingaddress = createAddressResult.Value;

        if (isDefault)
        {
            UnsetDefaultFromAllShippingAddresses();
        }

        _shippingAddresses.Add(shippingaddress);

        return Result.Success;
    }

    public Result<Success> AddDefaultShippingAddress(CustomerShippingAddressId shippingAddressId, AddressId addressId)
    {

        return AddShippingAddress(shippingAddressId, addressId, isDefault: true);
    }

    public Result<Success> AddShippingAddress(CustomerShippingAddressId shippingAddressId, AddressId addressId)
    {
        return AddShippingAddress(shippingAddressId, addressId, isDefault: false);
    }

    public Result<Success> SetAsDefaultShippingAddress(CustomerShippingAddressId shippingAddressId)
    {

        var newDefaultAddress = _shippingAddresses.FirstOrDefault(x => x.Id == shippingAddressId);

        if (newDefaultAddress is null)
        {
            return Error.NotFound("");//should change
        }

        UnsetDefaultFromAllShippingAddresses();
        newDefaultAddress.SetAsDefault();

        return Result.Success;
    }

    public Result<Success> RemoveShippingAddress(CustomerShippingAddressId shippingAddressId)
    {
        var address = _shippingAddresses.FirstOrDefault(y => y.Id == shippingAddressId);

        if (address is null)
        {
            return Error.NotFound("");//should change

        }

        _shippingAddresses.Remove(address);
        return Result.Success;
    }

    public void UnsetDefaultFromAllShippingAddresses()
    {
        _shippingAddresses.ForEach(address => address.UnsetDefault());
    }
}
