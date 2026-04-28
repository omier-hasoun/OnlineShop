
namespace Application;

internal static class ApplicationErrors
{

    public static class Validation
    {
        public static readonly Error MissingInput = Error.Validation("Application.Validation.MissingInput");
        public static readonly Error InvalidImageSize = Error.Validation("Application.Validation.InvalidImageSize");

        public static readonly Error InvalidImageFormat = Error.Validation("Application.Validation.InvalidImageFormat");

    }

    public static class Authentication
    {
        public static readonly Error Unauthorized = Error.Unauthorized("Application.Auth.Unauthorized", "You do not have permission to perform this action.");
        public static readonly Error Unauthenticated = Error.Unauthorized("Application.Auth.Unauthenticated", "You need to log in.");
        public static readonly Error ConfirmYourEmail = Error.ActionRequired("Application.ActionRequired.ConfirmYourEmail");
        public static readonly Error ChangeYourPassword = Error.ActionRequired("Application.ActionRequired.ChangeYourPassword");


    }

    public static class NotFound
    {
        public static readonly Error Product = Error.NotFound("Application.NotFound.Product");

    }


    public static class Conflict
    {
        public static readonly Error ProductTitleMustBeUnique = Error.Conflict("Application.Conflict.ProductTitleMustBeUnique");
    }
}
