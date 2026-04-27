
namespace Domain.Brands;

public readonly record struct BrandId
{
    public Guid Value { get; init; }

    public static implicit operator Guid(BrandId brandId) => brandId.Value;
    public static implicit operator BrandId(Guid value) => new BrandId(value);
    public BrandId(Guid value)
    {
        if (value == default)
            throw new ArgumentException("BrandId is invalid.", nameof(value));

        Value = value;
    }

    public static BrandId Parse(string value)
    {
        if(Guid.TryParse(value, out var brandId))
        {
            return new BrandId(brandId);
        }
        throw new ArgumentException("BrandId is invalid.", nameof(value));
    }
}
