
namespace Infrastructure;

internal static class TechnicalErrors
{
    public static class Images
    {
        public const string BaseErrorCode = "TechnicalErrors.Images";

        public static readonly Error InvalidImageSize = Error.Validation($"{BaseErrorCode}.{nameof(InvalidImageSize)}");

        public static readonly Error InvalidImageAspectRatio = Error.Validation($"{BaseErrorCode}.{nameof(InvalidImageAspectRatio)}");

        public static readonly Error InvalidImageFormat = Error.Validation($"{BaseErrorCode}.{nameof(InvalidImageFormat)}");

        public static readonly Error InvalidImage = Error.Validation($"{BaseErrorCode}.{nameof(InvalidImage)}");

        public static readonly Error rename5 = Error.Validation($"{BaseErrorCode}.{nameof(rename5)}");

        public static readonly Error rename6 = Error.Validation($"{BaseErrorCode}.{nameof(rename6)}");

        public static readonly Error rename7 = Error.Validation($"{BaseErrorCode}.{nameof(rename7)}");

        public static readonly Error rename8 = Error.Validation($"{BaseErrorCode}.{nameof(rename8)}");

        public static readonly Error rename9 = Error.Validation($"{BaseErrorCode}.{nameof(rename9)}");

    }
}
