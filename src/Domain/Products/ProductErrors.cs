

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
        Error.Validation("Product.MadeByCompany.Required", "Product made by company is required.");
    public static Error PriceRequired =>
        Error.Validation("Product.Price.Required", "Product price is required.");
    public static Error QuantityRequired =>
        Error.Validation("Product.Quantity.Required", "Product quantity is required.");

    public static Error PriceOutOfRange =>
        Error.Validation("Product.Price.OutOfRange", $"Product price must be at least {ProductRules.PriceMinValue} and at max {ProductRules.PriceMaxValue}.");

    public static Error NameOutOfRange =>
        Error.Validation("Product.Name.OutOfRange", $"Product name must be between {ProductRules.NameMinLength} and {ProductRules.NameMaxLength} characters long.");

    public static Error DescriptionOutOfRange =>
        Error.Validation("Product.Description.OutOfRange", $"Product description must be between {ProductRules.DescriptionMinLength} and {ProductRules.DescriptionMaxLength} characters long.");

    public static Error MadeByCompanyOutOfRange =>
        Error.Validation("Product.MadeByCompany.OutOfRange", $"Product made by company must be between {ProductRules.MadeByCompanyMinLength} and {ProductRules.MadeByCompanyMaxLength} characters long.");



    public static class CartItemErrors
    {
        public static Error UnitsOutOfRange =>
            Error.Forbidden("Product.Order.InsufficientStock", "Insufficient stock to fulfill the order.");
    }
}
