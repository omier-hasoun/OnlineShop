



namespace Domain.Products;

public sealed class Product : AuditableEntity
{
    private Product()
    {
    }

    public static Result<Product> Create(string name, string description, string madeByCompany, decimal price, int quantity, ProductId id = default)
    {
        return new Product
        {
            Id = id == default ? new ProductId(Guid.CreateVersion7()) : id,
            Name = name,
            Description = description,
            Price = price,
            MadeByCompany = madeByCompany,
            LastRestockedAt = TimeService.UtcNow,
            Quantity = quantity,
        };
    }

    public ProductId Id { get; private init; }
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public string MadeByCompany { get; private set; } = null!;
    public float? AverageRating { get; } = null;
    public int ReviewsCount { get; } = 0;
    public int Quantity { get; private set; }
    public DateTimeOffset LastRestockedAt { get; private set; }
    public decimal Price { get; private set; }

    public ICollection<ProductImage> ProductImages { get; private set; } = [];
    public ICollection<Review> Reviews { get; private set; } = [];


    public Result<Updated> Restock(int amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Restock amount must be greater than zero.");
        }

        Quantity += amount;
        LastRestockedAt = TimeService.UtcNow;

        return Result.Updated;
    }

    public Result<Updated> ChangePrice(decimal newPrice)
    {
        if (newPrice < ProductRules.PriceMinValue || newPrice > ProductRules.PriceMaxValue)
        {
            return ProductErrors.PriceOutOfRange;
        }

        Price = newPrice;
        return Result.Updated;
    }

    // public Result<CartItem> CreateCartItem(CustomerId customerId, ushort units)
    // {
    //     if (units == 0)
    //     {

    //     }
    //     if (units > Quantity)
    //     {
    //         return ProductErrors.CartItemErrors.UnitsOutOfRange;
    //     }

    //     Quantity -= (int)units;
    //     return CartItem.Create(
    //         customerId: customerId,
    //         productId: this.Id,
    //         units: (short)units);
    // }
}
