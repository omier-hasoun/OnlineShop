
namespace Domain;

public static class DomainErrors
{
    public static class ReturnItemRequest
    {
        public static readonly Error SerialNumbersCountNotEqualToQuantity =
            Error.Validation("DomainErrors.ReturnItemRequest.InvalidSNCount", "Serial Numbers count doesn't equal quantity.");

        public static readonly Error SerialNumbersRequired =
            Error.Validation("DomainErrors.ReturnItemRequest.SerialNumbersRequired", "Serial Numbers are required.");

        public static readonly Error ProductDoesNotRequireSerialNumbers =
            Error.Validation("DomainErrors.ReturnItemRequest.ProductDoesNotRequireSerialNumbers", "This product doesn't require Serial Numbers.");

        public static readonly Error InvalidSerialNumbers =
            Error.Validation("DomainErrors.ReturnItemRequest.InvalidSerialNumber", ".");
    }

    public static class ProductErrors
    {
        public static readonly Error ProductIdInvalid =
            Error.Validation("DomainErrors.ProductId.Invalid", "Product Id is invalid.");

        public static readonly Error TitleInvalid =
            Error.Validation("DomainErrors.Title.Invalid", "Product Title is invalid.");
        public static readonly Error DescriptionInvalid =
            Error.Validation("DomainErrors.Description.Invalid", "Product description is invalid.");
        public static readonly Error PriceInvalid =
            Error.Validation("DomainErrors.Price.Invalid", "Product price is invalid.");
        public static readonly Error QuantityInvalid =
            Error.Validation("DomainErrors.Quantity.Invalid", "Product quantity is invalid.");

        public static readonly Error TitleOutOfRange =
            Error.Validation("DomainErrors.Title.OutOfRange", $"Product title must be between {ProductRules.MinTitleLength} and {ProductRules.MaxTitleLength} characters long.");
        public static readonly Error DescriptionOutOfRange =
            Error.Validation("DomainErrors.Description.OutOfRange", $"Product description must be between {ProductRules.MinDescriptionLength} and {ProductRules.MaxDescriptionLength} characters long.");
        public static readonly Error ImagesOutOfRange =
            Error.Validation("DomainErrors.Images.OutOfRange", $"A product must have between {ProductVariantRules.MinNumberOfImages} and {ProductVariantRules.MaxNumberOfImages} images.");
    }
    public static class ProductVariantErrors
    {
        public static readonly Error PriceOutOfRange =
        Error.Validation("Product.Price.OutOfRange", $"Product price must be at least {ProductVariantRules.MinOriginalPriceValue} and at max {ProductVariantRules.MaxOriginalPriceValue}.");
    }
    public static class ProductReviewErrors
    {
        public static readonly Error CommentLengthOutOfRange =
            Error.Validation("Product.Review.CommentLength.OutOfRange", $"Review comment can't exceed {ProductReviewRules.MaxCommentLength} characters.");
    }
    public static class Orders
    {
        public static readonly Error OrderItemsCountOutOfRange =
            Error.Validation("Order.OrderItemsCount.OutOfRange", $"Order must contain between {OrderRules.MinOrderItemsCount} and {OrderRules.MaxOrderItemsCount} items.");
        public static readonly Error ShippingFeesOutOfRange =
            Error.Validation("Order.ShippingFees.OutOfRange", $"ShippingFees must be between {OrderRules.MaxShippingFeesValue} and {OrderRules.MaxShippingFeesValue} USD.");
        public static readonly Error TotalItemsPriceOutOfRange =
            Error.Validation("Order.TotalItemsPrice.OutOfRange", $"TotalItemsPrice must be between {OrderRules.MinTotalItemsPriceValue} and {OrderRules.MaxTotalItemsPriceValue} USD.");
    }
    
    public static class OrderItems
    {
        public static readonly Error SerialNumbersDoNotMatchQuantity = Error.Validation("OrderItem.SerialNumbers.DoNotMatchQuantity");

        public static readonly Error InvalidReturnQuantity = Error.Validation("OrderItem.SerialNumbers.DoNotMatchQuantity");
    }
    public static class BrandErrors
    {
        public static readonly Error BrandIdInvalid = Error.Validation("BrandId.Invalid", "Brand Id is invalid.");
    }

    public static class CategoryErrors
    {
        public static readonly Error CategoryIdInvalid = Error.Validation("CategoryId.Invalid", "Category Id is invalid.");
    }
    public static class CartItems
    {
        public static Error ProductVariantInfoIsNull = Error.Validation("CartItem.ProductVariantInfo.Null", "The given product variant information must not be null.");

        public static Error QuantityOutOfRange = Error.Forbidden("CartItem.Quantity.OutOfRange", $"The given quantity number must be between {CartItemRules.MinQuantityValue} and {CartItemRules.MaxQuantityValue} per cart item.");
    }
}
