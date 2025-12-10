
namespace Domain.Products;
public readonly record struct ProductId
{
    public Guid Value { get; }

    public ProductId(Guid value)
    {
        if (value == default)
            throw new ArgumentException("ProductId cannot be the default Guid.", nameof(value));

        Value = value;
    }
}
