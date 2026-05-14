
namespace Domain.Carts;

public sealed class Cart : AggregateRoot<CartId>, IHasModificationTime
{
    private Cart()
    {
        
    }
    private Cart(CartId id, Guid? userId, GuestAccountId? guestId, DateTime lastModifiedAt) : base(id)
    {
        UserId = userId;
        GuestId = guestId;
        LastModifiedAt = lastModifiedAt;
    }


    private static Result<Cart> Create(CartId id, Guid? userId, GuestAccountId? guestId)
    {
        var validationResult = Result.ValidateAll(
                                    () => id.IsValid()
                               );

        if (validationResult.Failed)
            return validationResult.Errors;


        return new Cart(id, userId, guestId, DateTime.UtcNow);
    }


    public static Result<Cart> CreateForGuest(CartId id, GuestAccountId guestId)
    {
        var validationResult = Result.ValidateAll(
                                    () => guestId.IsValid()
                               );

        if (validationResult.Failed)
            return validationResult.Errors;


        return Create(id, default, guestId);
    }

    public static Result<Cart> CreateForUser(CartId id, Guid userId)
    {
        var validationResult = Result.ValidateAll(
                                    () => userId.IsValidUserId()
                               );

        return Create(id, userId, default);
    }

    public DateTime LastModifiedAt { get; set; }
    public Guid? UserId { get; private init; }
    public GuestAccountId? GuestId { get; private init; }

    private List<CartItem> _items = [];
    public IReadOnlyCollection<CartItem> Items { get { return _items.AsReadOnly(); } private set { _items = value.ToList(); } }



    public Result<Success> AddItem(CartItemId cartItemId, ProductId productId, short quantity)
    {
        if (_items.Count >= CartRules.MaxNumberOfItems)
        {
            return DomainErrors.Carts.MaxNumberOfItemsReached;
        }

        var createItemResult = CartItem.Create(cartItemId, this.Id, productId, quantity);

        if (createItemResult.Failed)
            return createItemResult.Errors;

        _items.Add(createItemResult.Value);

        return Result.Success;
    }

    public Result<Success> RemoveItem(CartItemId cartItemId)
    {
        var cartItem = _items.FirstOrDefault(x => x.Id == cartItemId);

        if (cartItem is null)
        {
            return DomainErrors.cartItemIdInvalid;
        }

        _items.Remove(cartItem);

        return Result.Success;
    }

    public Result<Success> UpdateItem(CartItemId cartItemId, short newQuantity)
    {
        var cartItem = _items.FirstOrDefault(x => x.Id == cartItemId);

        if (cartItem is null)
        {
            return DomainErrors.cartItemIdInvalid;
        }

        var updateResult = cartItem.UpdateQuantity(newQuantity);

        if (updateResult.Failed)
            return updateResult.Errors;

        return Result.Success;
    }
}
