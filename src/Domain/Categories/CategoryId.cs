namespace Domain.Categories;

public readonly record struct CategoryId
{
    public long Value { get; }
    public CategoryId(long value)
    {
        Value = value;
    }

    public Result<Success> IsValid()
    {
        if (Value <= 0)
        {
            return DomainErrors.Categories.CategoryIdInvalid;
        }

        return Result.Success;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
