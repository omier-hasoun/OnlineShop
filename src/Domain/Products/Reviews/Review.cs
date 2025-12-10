namespace Domain.Products.Reviews;

public sealed class Review : BaseEntity
{
    private Review()
    {

    }

    public static Review Create(ProductId productId,
        CustomerId customerId,
        int rating,
        string comment,
        ReviewId id = default)
    {
        return new()
        {
            Id = id,
            ProductId = productId,
            CustomerId = customerId,
            Rating = rating,
            Comment = comment
        };
    }

    public ReviewId Id { get; private init; }
    public ProductId ProductId { get; private set; }
    public CustomerId CustomerId { get; private set; }
    public int Rating { get; private set; }
    public string Comment { get; private set; } = null!;

    public Product? ProductInfo { get; private set; }
    public Customer? CustomerInfo { get; private set; }

}
