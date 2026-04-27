

namespace Domain.Products;

public static class ProductErrors
{
    public static Error ProductIdInvalid =>
        Error.Validation("Product.ProductId.Invalid", "Product Id is invalid.");
    public static Error TitleInvalid =>
        Error.Validation("Product.Title.Invalid", "Product Title is invalid.");
    public static Error DescriptionInvalid =>
        Error.Validation("Product.Description.Invalid", "Product description is invalid.");
    public static Error BrandInvalid =>
        Error.Validation("Product.Manufacturer.Invalid", "Product manufacturer is invalid.");
    public static Error PriceInvalid =>
        Error.Validation("Product.Price.Invalid", "Product price is invalid.");
    public static Error QuantityInvalid =>
        Error.Validation("Product.Quantity.Invalid", "Product quantity is invalid.");

    public static Error TitleOutOfRange =>
        Error.Validation("Product.Title.OutOfRange", $"Product title must be between {ProductRules.MinTitleLength} and {ProductRules.MaxTitleLength} characters long.");
    public static Error DescriptionOutOfRange =>
        Error.Validation("Product.Description.OutOfRange", $"Product description must be between {ProductRules.MinDescriptionLength} and {ProductRules.MaxDescriptionLength} characters long.");
    //public static Error ImagesOutOfRange =>
    //    Error.Validation("Product.Images.OutOfRange", $"A product must have between {ProductRules.MinImagesCount} and {ProductRules.MaxNumberOfVariants} images.");
}
