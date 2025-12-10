namespace Domain.Products;

public readonly record struct ProductImageId
{
    public Guid Value { get; }

    public ProductImageId(Guid value)
    {
        if (value == default)
            throw new ArgumentException("ProductImageId cannot be the default Guid.", nameof(value));

        Value = value;
    }
}
