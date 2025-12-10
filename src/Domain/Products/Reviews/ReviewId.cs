namespace Domain.Products.Reviews;

public readonly record struct ReviewId
{
    public int Value { get; }

    public ReviewId(int value)
    {
        if (value <= 0)
            throw new ArgumentException("AddressId must be a positive integer.", nameof(value));

        Value = value;
    }
}
