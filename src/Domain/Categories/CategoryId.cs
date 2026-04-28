namespace Domain.Categories;

public readonly record struct CategoryId
{
    public long Value { get; }

    public static implicit operator long(CategoryId categoryId) => categoryId.Value;
    public static implicit operator CategoryId(long value) => new(value);
    internal CategoryId(long value)
    {
        Value = value;
    }

    public static Result<CategoryId> From(long value)
    {
        if (value <= 0)
        {
            return new CategoryId(value);
        }

        return DomainErrors.Categories.CategoryIdInvalid;
    }
}
