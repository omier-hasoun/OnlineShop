namespace Domain.Products.ProductImages;

public readonly record struct ProductImageId
{
    public Guid Value { get; }

    public ProductImageId(Guid value)
    {
        if (value.Version != 7 || value == default)
            throw new ArgumentException("ProductImageId is invalid.", nameof(value));

        Value = value;
    }

    public static implicit operator Guid(ProductImageId productImageId) => productImageId.Value;
    public static implicit operator ProductImageId(Guid value) => new ProductImageId(value);
}
