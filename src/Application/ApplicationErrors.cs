
namespace Application;

public static class ApplicationErrors
{
    public static class Validation
    {
        public const string BaseErrorCode = $"ApplicationErrors.Validation";
        public static string GenerateErrorCode(string error)
        {
            return $"BaseErrorCode.Validation.{error.Trim()}";
        }

        public static readonly Error MissingInput = Error.Validation("Application.Validation.MissingInput");
        public static readonly Error InvalidImageSize = Error.Validation("Application.Validation.InvalidImageSize");
        public static readonly Error InvalidImageFormat = Error.Validation("Application.Validation.InvalidImageFormat");
        public static readonly Error InvalidImage= Error.Validation("Application.Validation.InvalidImage");
        public static readonly Error PageSizeTooBig = Error.Validation("Application.Validation.PageSizeTooBig");
        public static readonly Error InvalidImageDimensions = Error.Validation("Application.Validation.InvalidImageDimensions");



    }

    public static class Authentication
    {
        public const string BaseErrorCode = $"ApplicationErrors.Auth";

        public static readonly Error Unauthorized = Error.Unauthorized("Application.Auth.Unauthorized", "You do not have permission to perform this action.");
        public static readonly Error Unauthenticated = Error.Unauthorized("Application.Auth.Unauthenticated", "You need to log in.");
        public static readonly Error ConfirmYourEmail = Error.ActionRequired("Application.ActionRequired.ConfirmYourEmail");
        public static readonly Error ChangeYourPassword = Error.ActionRequired("Application.ActionRequired.ChangeYourPassword");


    }

    public static class NotFound
    {
        public const string BaseErrorCode = $"ApplicationErrors";

        public static string GenerateErrorCode(string error)
        {
            return $"BaseErrorCode.{error.Trim()}.NotFound";
        }

        public static readonly Error Product = Error.NotFound(GenerateErrorCode(nameof(Product)));

    }


    public static class Conflict
    {
        public const string BaseErrorCode = $"ApplicationErrors";

        public static readonly Error ProductTitleMustBeUnique = Error.Conflict("Application.Conflict.ProductTitleMustBeUnique");
    }
}
