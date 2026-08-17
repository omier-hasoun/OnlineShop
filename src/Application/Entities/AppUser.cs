using Domain;
using Domain.Common.Entities.Addresses;
using Domain.UserShippingAddresses;

namespace Application.Entities;

public sealed class AppUser : IdentityUser<Guid>, IAggregateRoot, ISoftDelete, IHasCreationTime, IHasModificationTime
{
    public AppUser()
    {
        if(Id == default)
        {
            Id = Guid.CreateVersion7();
        }
    }


    public IReadOnlyCollection<UserClaim> Claims { get; private set; } = [];
    public IReadOnlyCollection<UserToken> Tokens { get; private set; } = [];

    private List<UserShippingAddress> _addresses = [];
    public IReadOnlyCollection<UserShippingAddress> ShippingAddresses { get { return _addresses.AsReadOnly(); } private set { _addresses = value.ToList(); } }

    public bool IsDeleted { get; private set; }

    public DateTime CreatedAt { get; set; }
    public DateTime LastModifiedAt { get; set; }




    public Result<Success> AddShippingAddress(UserShippingAddressId shippingAddressId, AddressId addressId, bool isDefault)
    {
        if (_addresses.Count >= UserRules.MaxNumberOfAddresses)
        {
            return DomainErrors.Users.MaxNumberOfAddressesReached;
        }

        var createAddressResult = UserShippingAddress.Create(shippingAddressId, this.Id, addressId, isDefault);

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

    public Result<Success> SetAsDefaultShippingAddress(UserShippingAddressId shippingAddressId)
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

    public Result<Success> RemoveShippingAddress(UserShippingAddressId shippingAddressId)
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






    #region events

    private List<IDomainEvent> _domainEvents = [];
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();


    public void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        if (domainEvent is null)
            return;

        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
    #endregion
}

