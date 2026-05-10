using Domain;
using Domain.Carts.CartItems;
using Domain.Common.Entities.Addresses;
using Domain.CustomerShippingAddresses;

namespace Application.Entities;

public sealed class AppUser : IdentityUser<Guid>, IEntity, ISoftDelete, IHasCreationTime, IHasModificationTime
{
    private List<IDomainEvent> _domainEvents = [];
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public IReadOnlyCollection<UserClaim> Claims { get; private set; } = [];
    public IReadOnlyCollection<UserToken> Tokens { get; private set; } = [];
    public IReadOnlyCollection<Role> Roles { get; private set; } = [];

    private List<ShippingAddress> _addresses = [];
    public IReadOnlyCollection<ShippingAddress> ShippingAddresses { get { return _addresses.AsReadOnly(); } private set { _addresses = value.ToList(); } }

    public bool IsDeleted { get; private set; }

    public DateTime CreatedAt { get; set; }
    public DateTime LastModifiedAt { get; set; }

    public AppUser()
    {
        if(Id == default)
        {
            Id = Guid.CreateVersion7();
        }
    }
    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        if (domainEvent is null)
            return;

        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public Result<Success> AddShippingAddress(ShippingAddressId shippingAddressId, AddressId addressId, bool isDefault)
    {
        if (_addresses.Count >= UserRules.MaxNumberOfAddresses)
        {
            return DomainErrors.Users.MaxNumberOfAddressesReached;
        }

        var createAddressResult = ShippingAddress.Create(shippingAddressId, this.Id, addressId, isDefault);

        if (createAddressResult.Failed)
        {
            return createAddressResult.Errors;
        }
        var shippingaddress = createAddressResult.Value;

        if (isDefault)
        {
            ResetDefaultAddress();
        }

        _addresses.Add(shippingaddress);

        return Result.Success;
    }

    public Result<Success> SetAsDefaultShippingAddress(ShippingAddressId shippingAddressId)
    {

        var newDefaultAddress = _addresses.FirstOrDefault(x => x.Id == shippingAddressId);

        if (newDefaultAddress is null)
        {
            return Error.NotFound("");//should change
        }

        ResetDefaultAddress();
        newDefaultAddress.SetAsDefault();

        return Result.Success;
    }

    public Result<Success> RemoveShippingAddress(ShippingAddressId shippingAddressId)
    {
        var address = _addresses.FirstOrDefault(y => y.Id == shippingAddressId);

        if (address is null)
        {
            return Error.NotFound("");//should change

        }

        _addresses.Remove(address);
        return Result.Success;
    }

    public void ResetDefaultAddress()
    {
        _addresses.ForEach(address => address.UnsetDefault());
    }

}

