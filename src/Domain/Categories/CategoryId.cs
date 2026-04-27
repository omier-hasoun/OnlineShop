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
        if(ulong.TryParse(value,out ulong categoryId))
        {
            return new CategoryId((long)categoryId);
        }
        throw new ArgumentException("CategoryId is invalid.", nameof(value));
    }
}
