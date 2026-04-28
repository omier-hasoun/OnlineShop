namespace Domain.Categories;

public readonly record struct CategoryId
{
    public long Value { get; }

    public static implicit operator long(CategoryId categoryId) => categoryId.Value;
    public static implicit operator CategoryId(long value) => new(value);
    public CategoryId(long value)
    {
        if (value <= 0)
            throw new ArgumentException("CategoryId is invalid.", nameof(value));

        Value = value;
    }

    public static CategoryId Parse(string value)
    {
        if (TryParse(value, out var id))
            return id;
        throw new ArgumentException("CategoryId is invalid.", nameof(value));
    }
    public static bool TryParse(string value, out CategoryId id)
    {
        if (long.TryParse(value, out var brandId))
        {
            id = new CategoryId(brandId);
            return true;
        }
        id = new();
        return false;
    }
}
