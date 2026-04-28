
namespace Domain.Brands;

public readonly record struct BrandId
{
    public Guid Value { get; init; }

    public static implicit operator Guid(BrandId brandId) => brandId.Value;
    public static implicit operator BrandId(Guid value) => new(value);
    internal BrandId(Guid value)
    {
        Value = value;
    }

    public static Result<BrandId> From(Guid value)
    {
        if (value.Version == 7)
        {
            return new BrandId(value);
        }

        return DomainErrors.Brands.BrandIdInvalid;
    }

}
