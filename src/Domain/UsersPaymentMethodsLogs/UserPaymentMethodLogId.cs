
namespace Domain.UsersPaymentMethodsLogs;

public readonly record struct UserPaymentMethodLogId
{
    public long Value { get; }
    public UserPaymentMethodLogId(long value)
    {
        Value = value;
    }

    public Result<Success> IsValid()
    {
        if (Value <= 0)
        {
            return DomainErrors.UserPaymentMethodLogs.UserPaymentMethodLogIdInvalid;
        }

        return Result.Success;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
