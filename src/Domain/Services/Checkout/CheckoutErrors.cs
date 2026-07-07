
namespace Domain.Services.Checkout;

public sealed class CheckoutErrors
{
    public const string BaseErrorCode = "CheckoutErrors";

    public static readonly Error QuantityLimitExceeded = Error.Validation($"{BaseErrorCode}.{nameof(QuantityLimitExceeded)}");

    public static readonly Error OrderHasNoItems = Error.Validation($"{BaseErrorCode}.{nameof(OrderHasNoItems)}");

    public static readonly Error rename3 = Error.Validation($"{BaseErrorCode}.{nameof(rename3)}");

    public static readonly Error rename4 = Error.Validation($"{BaseErrorCode}.{nameof(rename4)}");

    public static readonly Error InvalidOrder = Error.Validation($"{BaseErrorCode}.{nameof(InvalidOrder)}");

    public static readonly Error rename6 = Error.Validation($"{BaseErrorCode}.{nameof(rename6)}");

    public static readonly Error rename7 = Error.Validation($"{BaseErrorCode}.{nameof(rename7)}");

    public static readonly Error rename8 = Error.Validation($"{BaseErrorCode}.{nameof(rename8)}");

    public static readonly Error ProductNotPurchasbale = Error.Validation($"{BaseErrorCode}.{nameof(ProductNotPurchasbale)}");
}
