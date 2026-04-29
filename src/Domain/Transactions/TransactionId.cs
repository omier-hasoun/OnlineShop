
namespace Domain.Transactions;

public readonly record struct TransactionId
{
    public long Value { get; }
    public TransactionId(long value)
    {
        Value = value;
    }

    public Result<Success> IsValid()
    {
        if (Value <= 0)
        {
            return DomainErrors.Transactions.TransactionIdInvalid;
        }

        return Result.Success;
    }
}


