
namespace Domain.Brands;

public readonly record struct BrandId
{
    public Guid Value { get; init; }

    public BrandId(Guid value)
    {
        Value = value;
    }

    public Result<Success> IsValid()
    {
        if (Value.Version != 7)
        {
            return DomainErrors.Brands.BrandIdInvalid;
        }

        return Result.Success;
    }
}
