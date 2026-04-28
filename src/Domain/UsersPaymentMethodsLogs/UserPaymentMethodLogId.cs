
namespace Domain.UsersPaymentMethodsLogs;

public readonly record struct UserPaymentMethodLogId
{
    public long Value { get; }

    public static implicit operator long(UserPaymentMethodLogId userPaymentMethodLogId) => userPaymentMethodLogId.Value;
    public static implicit operator UserPaymentMethodLogId(long value) => new(value);
    public UserPaymentMethodLogId(long value)
    {
        if (value <= 0)
            throw new ArgumentException("UserPaymentMethodLogId is invalid.", nameof(value));

        Value = value;
    }

    public static Result<UserPaymentMethodLogId> From(long value)
    {
        if (value <= 0)
        {
            return new UserPaymentMethodLogId(value);
        }

        return DomainErrors.Categories.CategoryIdInvalid;
    }
}
