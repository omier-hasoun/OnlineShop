
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
}


