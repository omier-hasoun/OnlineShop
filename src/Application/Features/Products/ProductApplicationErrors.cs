namespace Application.Features.Products;

public static class ProductApplicationErrors
{
    public static Error ProductNameAlreadyExists => Error.Forbidden(
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
}
