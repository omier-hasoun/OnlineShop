
namespace Domain.Transactions;

public readonly record struct TransactionId
{
    public long Value { get; init; }

    public static implicit operator long(TransactionId transactionId) => transactionId.Value;
    public static implicit operator TransactionId(long value) => new(value);
    public TransactionId(long value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("TransactionId is invalid.", nameof(value));
        }
        Value = value;
    }

    public static TransactionId Parse(string value)
    {
        if (TryParse(value, out var id))
            return id;
        throw new ArgumentException("TransactionId is invalid.", nameof(value));
    }
    public static bool TryParse(string value, out TransactionId id)
    {
        if (long.TryParse(value, out var brandId))
        {
            id = new TransactionId(brandId);
            return true;
        }
        id = new();
        return false;
    }
}


