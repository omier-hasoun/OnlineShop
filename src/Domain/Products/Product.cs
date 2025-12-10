namespace Domain.Products;

public sealed class Product : AuditableEntity
{
    private Product()
    {
    }

    public static Product Create(string name, string description, string madeByCompany, float averageRating, decimal price, ProductId id = default)
    {
        return new()
        {
            Id = id == default ? new ProductId(Guid.CreateVersion7()) : id,
            Name = name,
            Description = description,
            Price = price,
            MadeByCompany = madeByCompany,
            AverageRating = averageRating
        };
    }

    public ProductId Id { get; private init; }
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public string MadeByCompany { get; private set; } = null!;
    public float AverageRating { get; private set; }
    public int Quantity { get; private set; }
    public DateTimeOffset LastRestockedAt { get; private set; }
    public decimal Price { get; private set; }

    public ICollection<ProductImage> ProductImages { get; private set; } = [];
    public ICollection<Review> Reviews { get; private set; } = [];
}
