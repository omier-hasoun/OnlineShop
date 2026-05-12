
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
        if (Value.Version != 7)// this will also ensure that a guid is not default
        {
            return DomainErrors.Brands.BrandIdInvalid;
        }

        return Result.Success;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
