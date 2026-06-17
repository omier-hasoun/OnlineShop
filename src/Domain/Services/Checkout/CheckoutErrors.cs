
namespace Domain.Services.Checkout;

public sealed class CheckoutErrors
{
    public const string BaseErrorCode = "CheckoutErrors";

    public static readonly Error rename1 = Error.Validation($"{BaseErrorCode}.{nameof(rename1)}");

    public static readonly Error rename2 = Error.Validation($"{BaseErrorCode}.{nameof(rename2)}");

    public static readonly Error rename3 = Error.Validation($"{BaseErrorCode}.{nameof(rename3)}");

    public static readonly Error rename4 = Error.Validation($"{BaseErrorCode}.{nameof(rename4)}");

    public static readonly Error rename5 = Error.Validation($"{BaseErrorCode}.{nameof(rename5)}");

    public static readonly Error rename6 = Error.Validation($"{BaseErrorCode}.{nameof(rename6)}");

    public static readonly Error rename7 = Error.Validation($"{BaseErrorCode}.{nameof(rename7)}");

    public static readonly Error rename8 = Error.Validation($"{BaseErrorCode}.{nameof(rename8)}");

    public static readonly Error InvalidProduct = Error.Validation($"{BaseErrorCode}.{nameof(InvalidProduct)}");
}
