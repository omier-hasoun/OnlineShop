

namespace Domain.Products;

public static class ProductErrors
{
    public static Error ProductIdRequired =>
        Error.Validation("Product.ProductId.Required", "Product Id is required.");
    public static Error NameRequired =>
        Error.Validation("Product.Name.Required", "Product name is required.");
    public static Error DescriptionRequired =>
        Error.Validation("Product.Description.Required", "Product description is required.");
    public static Error MadeByCompanyRequired =>
        Error.Validation("Product.Manufacturer.Required", "Product manufacturer is required.");
    public static Error PriceRequired =>
        Error.Validation("Product.Price.Required", "Product price is required.");
    public static Error QuantityRequired =>
        Error.Validation("Product.Quantity.Required", "Product quantity is required.");

    public static Error PriceOutOfRange =>
        Error.Validation("Product.Price.OutOfRange", $"Product price must be at least {ProductRules.MinDefaultPriceValue} and at max {ProductRules.MaxDefaultPriceValue}.");

    public static Error NameOutOfRange =>
        Error.Validation("Product.Name.OutOfRange", $"Product name must be between {ProductRules.MinNameLength} and {ProductRules.MaxNameLength} characters long.");

    public static Error DescriptionOutOfRange =>
        Error.Validation("Product.Description.OutOfRange", $"Product description must be between {ProductRules.MinDescriptionLength} and {ProductRules.MaxDescriptionLength} characters long.");

    public static Error ManufacturerOutOfRange =>
        Error.Validation("Product.Manufacturer.OutOfRange", $"Product Manufacturer must be between {ProductRules.MinManufacturerLength} and {ProductRules.MaxManufacturerLength} characters long.");

    public static Error ImagesOutOfRange =>
        Error.Validation("Product.Images.OutOfRange", $"A product can have between {ProductRules.MinProductImagesCount} and {ProductRules.MaxProductImagesCount} images.");

}
