
namespace Application;

public static class ApplicationErrors
{
    public static class Validation
    {
        public const string BaseErrorCode = $"ApplicationErrors.Validation";
        public static string GenerateErrorCode(string error)
        {
            return $"{BaseErrorCode}.{error}";
        }

        public static readonly Error MissingInput = Error.Validation("Application.Validation.MissingInput");
        public static readonly Error InvalidImageSize = Error.Validation("Application.Validation.InvalidImageSize");
        public static readonly Error InvalidImageFormat = Error.Validation("Application.Validation.InvalidImageFormat");
        public static readonly Error InvalidImage= Error.Validation("Application.Validation.InvalidImage");
        public static readonly Error PageSizeTooBig = Error.Validation("Application.Validation.PageSizeTooBig");
        public static readonly Error InvalidImageDimensions = Error.Validation("Application.Validation.InvalidImageDimensions");

        public static readonly Error rename1 = Error.Validation(GenerateErrorCode(nameof(rename1)));
        public static readonly Error rename2 = Error.Validation(GenerateErrorCode(nameof(rename2)));
        public static readonly Error rename3 = Error.Validation(GenerateErrorCode(nameof(rename3)));
        public static readonly Error rename4 = Error.Validation(GenerateErrorCode(nameof(rename4)));
        public static readonly Error rename5 = Error.Validation(GenerateErrorCode(nameof(rename5)));
        public static readonly Error rename6 = Error.Validation(GenerateErrorCode(nameof(rename6)));


    }

    public static class Authentication
    {
        public const string BaseErrorCode = $"ApplicationErrors.Auth";
        public static string GenerateErrorCode(string error)
        {
            return $"{BaseErrorCode}.{error}";
        }

        public static readonly Error Unauthorized = Error.Unauthorized("Application.Auth.Unauthorized", "You do not have permission to perform this action.");
        public static readonly Error Unauthenticated = Error.Unauthorized("Application.Auth.Unauthenticated", "You need to log in.");
        public static readonly Error ConfirmYourEmail = Error.ActionRequired("Application.ActionRequired.ConfirmYourEmail");
        public static readonly Error ChangeYourPassword = Error.ActionRequired("Application.ActionRequired.ChangeYourPassword");

        public static readonly Error rename1 = Error.Unauthorized(GenerateErrorCode(nameof(rename1)));
        public static readonly Error rename2 = Error.Unauthorized(GenerateErrorCode(nameof(rename2)));
        public static readonly Error rename3 = Error.Unauthorized(GenerateErrorCode(nameof(rename3)));

    }

    public static class NotFound
    {
        public const string BaseErrorCode = $"ApplicationErrors";
        public static string GenerateErrorCode(string error)
        {
            return $"{BaseErrorCode}.{error}.NotFound";
        }

        public static readonly Error Product = Error.NotFound(GenerateErrorCode(nameof(Product)));
        public static readonly Error ProductVariant = Error.NotFound(GenerateErrorCode(nameof(ProductVariant)));
        public static readonly Error Order = Error.NotFound(GenerateErrorCode(nameof(Order)));
        public static readonly Error OrderItem = Error.NotFound(GenerateErrorCode(nameof(OrderItem)));
        public static readonly Error ProductReview = Error.NotFound(GenerateErrorCode(nameof(ProductReview)));

        public static readonly Error User = Error.NotFound(GenerateErrorCode(nameof(User)));
        public static readonly Error rename2 = Error.NotFound(GenerateErrorCode(nameof(rename2)));
        public static readonly Error rename3 = Error.NotFound(GenerateErrorCode(nameof(rename3)));

    }


    public static class Conflict
    {
        public const string BaseErrorCode = $"ApplicationErrors.Conflict";
        public static string GenerateErrorCode(string error)
        {
            return $"{BaseErrorCode}.{error}";
        }

        public static readonly Error ProductTitleMustBeUnique = Error.Conflict("Application.Conflict.ProductTitleMustBeUnique");
    }

    public static class InternalError
    {
        public const string BaseErrorCode = $"ApplicationErrors.InternalError";
        public static string GenerateErrorCode(string error)
        {
            return $"{BaseErrorCode}.{error}";
        }

        public static readonly Error SavingImageFileFailed = Error.Conflict(GenerateErrorCode(nameof(SavingImageFileFailed)));
        public static readonly Error rename1 = Error.Unexpected(GenerateErrorCode(nameof(rename1)));
        public static readonly Error rename2 = Error.Unexpected(GenerateErrorCode(nameof(rename2)));
        public static readonly Error rename3 = Error.Unexpected(GenerateErrorCode(nameof(rename3)));

    }
}
