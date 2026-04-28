
namespace Domain;
// need to replece ErrorCodes with the BaseErrorCode 
public static class DomainErrors
{
    public static class ReturnItemRequest
    {
        public const string BaseErrorCode = "DomainErrors.ReturnItemRequest";

        public static readonly Error SerialNumbersCountNotEqualToQuantity =
            Error.Validation("DomainErrors.ReturnItemRequest.InvalidSNCount", "Serial Numbers count doesn't equal quantity.");

        public static readonly Error SerialNumbersRequired =
            Error.Validation("DomainErrors.ReturnItemRequest.SerialNumbersRequired", "Serial Numbers are required.");

        public static readonly Error ProductDoesNotRequireSerialNumbers =
            Error.Validation("DomainErrors.ReturnItemRequest.ProductDoesNotRequireSerialNumbers", "This product doesn't require Serial Numbers.");

        public static readonly Error InvalidSerialNumbers =
            Error.Validation("DomainErrors.ReturnItemRequest.InvalidSerialNumber", ".");
    }



    public static class Products
    {
        public const string BaseErrorCode = "DomainErrors.Products";

        public static readonly Error ProductIdInvalid = Error.Validation($"{BaseErrorCode}.ProductIdInvalid");

        public static readonly Error TitleInvalid = Error.Validation($"{BaseErrorCode}.TitleInvalid");

        public static readonly Error DescriptionInvalid = Error.Validation($"{BaseErrorCode}.DescriptionInvalid");

        public static readonly Error PriceInvalid = Error.Validation($"{BaseErrorCode}.PriceInvalid", "Product price is invalid.");

        public static readonly Error QuantityInvalid = Error.Validation($"{BaseErrorCode}.QuantityInvalid", "Product quantity is invalid.");

        public static readonly Error AverageRatingInvalid = Error.Validation($"{BaseErrorCode}.AverageRatingInvalid");

        public static readonly Error InvalidImageFilePath = Error.Validation($"{BaseErrorCode}.InvalidImageFilePath");



        public static readonly Error TitleOutOfRange = Error.Validation($"{BaseErrorCode}.TitleOutOfRange");

        public static readonly Error DescriptionOutOfRange = Error.Validation($"{BaseErrorCode}.DescriptionOutOfRange");

        public static readonly Error ImagesOutOfRange = Error.Validation($"{BaseErrorCode}.ImagesOutOfRange");

    }



    public static class ProductVariants
    {
        public const string BaseErrorCode = "DomainErrors.ProductVariants";

        public static readonly Error ProductVariantIdInvalid = Error.Validation($"{BaseErrorCode}.ProductVariantIdInvalid");
        public static readonly Error PriceOutOfRange = Error.Validation($"{BaseErrorCode}.PriceOutOfRange");
    }





    public static class ProductReviews
    {
        public const string BaseErrorCode = "DomainErrors.ProductReviews";

        public static readonly Error ProductReviewIdInvalid = Error.Validation($"{BaseErrorCode}.ProductReviewIdInvalid");

        public static readonly Error CommentLengthOutOfRange =Error.Validation($"{BaseErrorCode}.CommentLengthOutOfRange");
    }




    public static class Orders
    {
        public const string BaseErrorCode = "DomainErrors.Orders";


        public static readonly Error OrderIdInvalid = Error.Validation($"{BaseErrorCode}.OrderIdInvalid");

        public static readonly Error OrderItemsNumberOutOfRange = Error.Validation($"{BaseErrorCode}.OutOfRange");

        public static readonly Error ShippingFeesOutOfRange = Error.Validation($"{BaseErrorCode}.OutOfRange");

        public static readonly Error TotalItemsPriceOutOfRange = Error.Validation($"{BaseErrorCode}.OutOfRange");
    }




    
    public static class OrderItems
    {
        public const string BaseErrorCode = "DomainErrors.OrderItems";

        public static readonly Error SerialNumbersDoNotMatchQuantity = Error.Validation("DomainErrors.SerialNumbers.DoNotMatchQuantity");

        public static readonly Error InvalidReturnQuantity = Error.Validation("DomainErrors.SerialNumbers.DoNotMatchQuantity");
    }




    public static class Brands
    {
        public const string BaseErrorCode = "DomainErrors.Brands";

        public static readonly Error BrandIdInvalid = Error.Validation("DomainErrors.Invalid", "Brand Id is invalid.");
    }



    public static class Categories
    {
        public const string BaseErrorCode = "DomainErrors.Categories";

        public static readonly Error CategoryIdInvalid = Error.Validation("DomainErrors.Invalid", "Category Id is invalid.");
    }



    public static class CartItems
    {
        public const string BaseErrorCode = "DomainErrors.CartItems";

        public static readonly Error CartItemIdInvalid = Error.Validation($"{BaseErrorCode}.CartItemIdInvalid");
        public static readonly Error QuantityOutOfRange = Error.Validation($"{BaseErrorCode}.QuantityOutOfRange");
    }



    public static class Warehouses
    {
        public const string BaseErrorCode = "DomainErrors.Warehouses";

        public static readonly Error WarehouseIdInvalid = Error.Validation($"{BaseErrorCode}.WarehouseIdInvalid");

    }



    public static class Customers
    {
        public const string BaseErrorCode = "DomainErrors.Customers";

        public static readonly Error CustomerIdInvalid = Error.Validation($"{BaseErrorCode}.CustomerIdInvalid");


    }



    public static class Common
    {
        public const string BaseErrorCode = "DomainErrors.Common";

        public static readonly Error MoneyAmountInvalid = Error.Validation($"{BaseErrorCode}.MoneyAmountInvalid");

    }



    public static class CustomerShippingAddresses
    {
        public const string BaseErrorCode = "DomainErrors.CustomerShippingAddresses";
        public static readonly Error CustomerShippingAddressesIdInvalid = Error.Validation($"{BaseErrorCode}.CustomerShippingAddressesIdInvalid");

    }

}
