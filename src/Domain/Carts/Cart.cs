
namespace Domain.Carts;

public sealed class Cart : AggregateRoot<CartId>, IHasModificationTime
{
    private Cart()
    {
        
    }
    private Cart(CartId id, Guid? userId, GuestAccountId? guestId, int quantity, DateTime lastModifiedAt) : base(id)
    {
        UserId = userId;
        GuestId = guestId;
        Quantity = quantity;
        LastModifiedAt = lastModifiedAt;
    }


    private static Result<Cart> Create(CartId id, Guid? userId, GuestAccountId? guestId)
    {
        var validationResult = Result.ValidateAll(
                                    () => id.IsValid()
                               );

        if (validationResult.Failed)
            return validationResult.Errors;


        return new Cart(id, userId, guestId, 0, DateTime.UtcNow);
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

    public Result<Success> UpgradeToUser(Guid userId)
    {
        if (userId == default)
            return DomainErrors.MissingInput.WithParameters(userId);

        GuestId = null;
        UserId = userId;
        return Result.Success;
    }

    public DateTime LastModifiedAt { get; set; }
    public Guid? UserId { get; private set; }
    public GuestAccountId? GuestId { get; private set; }

    public int Quantity { get; private set; }

    private List<CartItem> _items = [];
    public IReadOnlyCollection<CartItem> Items { get { return _items.AsReadOnly(); } private set { _items = value.ToList(); } }

    private void CalculateQuantity()
        => Quantity = Items.Sum(x => x.Quantity);

    public Result<Success> AddItem(CartItemId cartItemId, ProductId productId, short quantity)
    {
        if (_items.Count >= CartRules.MaxNumberOfItems)
        {
            return DomainErrors.Carts.MaxNumberOfItemsReached;
        }

        var item = _items.FirstOrDefault(i => i.ProductId == productId);

        // add quantity if item already exists
        if(item is not null)
        {
            var newQuantity = item.Quantity + quantity;

            var result = item.UpdateQuantity((short)newQuantity);

            if (result.Failed)
                return result.Errors;
        }
        else
        {
            var createItemResult = CartItem.Create(cartItemId, this.Id, productId, quantity);

            if (createItemResult.Failed)
                return createItemResult.Errors;

            _items.Add(createItemResult.Value);

        }

        CalculateQuantity();

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

        CalculateQuantity();


        return Result.Success;
    }

    public Result<Success> UpdateItem(CartItemId cartItemId, short newQuantity)
    {
        var item = _items.FirstOrDefault(x => x.Id == cartItemId);

        if (item is null)
        {
            return DomainErrors.cartItemIdInvalid;
        }

        var updateResult = item.UpdateQuantity(newQuantity);

        if (updateResult.Failed)
            return updateResult.Errors;

        CalculateQuantity();


        return Result.Success;
    }
}
