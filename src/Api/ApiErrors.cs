using Shared.Results;

namespace Api;

public static class ApiErrors
{

    public static class Client
    {
        public const string BaseErrorCode = "ClientErrors";
        public static readonly Error UnableToIdentifyUser = Error.Unauthorized($"{BaseErrorCode}.{nameof(UnableToIdentifyUser)}");

        public static readonly Error rename2 = Error.Validation($"{BaseErrorCode}.{nameof(rename2)}");

        public static readonly Error rename3 = Error.Validation($"{BaseErrorCode}.{nameof(rename3)}");

        public static readonly Error rename4 = Error.Validation($"{BaseErrorCode}.{nameof(rename4)}");

        public static readonly Error rename5 = Error.Validation($"{BaseErrorCode}.{nameof(rename5)}");

        public static readonly Error rename6 = Error.Validation($"{BaseErrorCode}.{nameof(rename6)}");

        public static readonly Error rename7 = Error.Validation($"{BaseErrorCode}.{nameof(rename7)}");

        public static readonly Error rename8 = Error.Validation($"{BaseErrorCode}.{nameof(rename8)}");

        public static readonly Error rename9 = Error.Validation($"{BaseErrorCode}.{nameof(rename9)}");
    } 


}
