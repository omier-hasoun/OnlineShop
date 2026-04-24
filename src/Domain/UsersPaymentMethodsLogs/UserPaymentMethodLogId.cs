
namespace Domain.UsersPaymentMethodsLogs;

public readonly record struct UserPaymentMethodLogId
{
    public long Value { get; }

    public static implicit operator long(UserPaymentMethodLogId userPaymentMethodLogId) => userPaymentMethodLogId.Value;
    public static implicit operator UserPaymentMethodLogId(long value) => new UserPaymentMethodLogId(value);
    public UserPaymentMethodLogId(long value)
    {
        if (value <= 0)
            throw new ArgumentException("UserPaymentMethodLogId is invalid.", nameof(value));

        Value = value;
    }
}
