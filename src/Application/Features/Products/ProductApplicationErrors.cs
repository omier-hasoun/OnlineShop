namespace Application.Features.Products;

public static class ProductApplicationErrors
{
    public static Error ProductTitleMustBeUnique => Error.Forbidden(
        code: "Product.Name.AlreadyExists",
        description: "A product with the given name already exists."
    );

    public static Error ProductNotFound => Error.NotFound(
        code: "Product.NotFound",
        description: "A product with the given ID was not found."
    );

    public static Error ProductDeletionFailed => Error.Failure(
        code: "Product.Deletion.Failed",
        description: "The product could not be deleted due to an internal error."
    );

    public static Error ProductCreationFailed => Error.Failure(
        code: "Product.Creation.Failed",
        description: "The product could not be created due to an internal error."
    );

    public static Error InvalidImage => Error.Validation(
        code: "Product.Images.Invalid",
        description: $"Invalid image. please ensure that your image format matches one of these : {ProductApplicationRules.AllowedImageExtensions}."
    );

    public static Error InvalidImageSize => Error.Forbidden(
        code: "Product.Images.InvalidSize",
        description: $"A product Image must be at max {ProductApplicationRules.MaxImageSizeMb} MB."
    );
}
