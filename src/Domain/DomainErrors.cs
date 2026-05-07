
namespace Domain;
// need to replece ErrorCodes with the BaseErrorCode 
public static class DomainErrors
{
    public const string BaseErrorCode = "DomainErrors";

    public static readonly Error ProductIdInvalid = Products.ProductIdInvalid;
    public static readonly Error ReturnItemRequestIdInvalid = ReturnItemRequests.ReturnItemRequestIdInvalid;
    public static readonly Error ProductVariantIdInvalid = ProductVariants.ProductVariantIdInvalid;
    public static readonly Error OrderIdInvalid = Orders.OrderIdInvalid;
    public static readonly Error ProductReviewIdInvalid = ProductReviews.ProductReviewIdInvalid;
    public static readonly Error InvalidAction = Error.Validation($"{BaseErrorCode}.{nameof(InvalidAction)}");
    public static readonly Error rename7 = Error.Validation($"{BaseErrorCode}.{nameof(rename7)}");
    public static readonly Error rename8 = Error.Validation($"{BaseErrorCode}.{nameof(rename8)}");
    public static readonly Error rename9 = Error.Validation($"{BaseErrorCode}.{nameof(rename9)}");
    public static readonly Error rename10 = Error.Validation($"{BaseErrorCode}.{nameof(rename10)}");
    public static class ReturnItemRequests
    {
        public const string BaseErrorCode = "DomainErrors.ReturnItemRequest";

        public static readonly Error SerialNumbersCountNotEqualToQuantity =
            Error.Validation("DomainErrors.ReturnItemRequest.InvalidSNCount", "Serial Numbers count doesn't equal quantity.");

        public static readonly Error SerialNumbersRequired =
            Error.Validation("DomainErrors.ReturnItemRequest.SerialNumbersRequired", "Serial Numbers are required.");

        public static readonly Error ProductDoesNotRequireSerialNumbers =
            Error.Validation("DomainErrors.ReturnItemRequest.ProductDoesNotRequireSerialNumbers", "This product doesn't require Serial Numbers.");

        public static readonly Error InvalidSerialNumbers = Error.Validation("DomainErrors.ReturnItemRequest.InvalidSerialNumber", ".");
        public static readonly Error ReturnItemRequestIdInvalid = Error.Validation($"{BaseErrorCode}.ReturnItemRequestIdInvalid");


        public static readonly Error rename1 = Error.Validation($"{BaseErrorCode}.{nameof(rename1)}");
        public static readonly Error rename2 = Error.Validation($"{BaseErrorCode}.{nameof(rename2)}");
        public static readonly Error rename3 = Error.Validation($"{BaseErrorCode}.{nameof(rename3)}");
        public static readonly Error rename4 = Error.Validation($"{BaseErrorCode}.{nameof(rename4)}");
        public static readonly Error rename5 = Error.Validation($"{BaseErrorCode}.{nameof(rename5)}");
        public static readonly Error rename6 = Error.Validation($"{BaseErrorCode}.{nameof(rename6)}");
        public static readonly Error rename7 = Error.Validation($"{BaseErrorCode}.{nameof(rename7)}");
        public static readonly Error rename8 = Error.Validation($"{BaseErrorCode}.{nameof(rename8)}");
        public static readonly Error rename9 = Error.Validation($"{BaseErrorCode}.{nameof(rename9)}");
        public static readonly Error rename10 = Error.Validation($"{BaseErrorCode}.{nameof(rename10)}");

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


        public static readonly Error MaxNumberOfVariantsReached = Error.Validation($"{BaseErrorCode}.{nameof(MaxNumberOfVariantsReached)}");
        public static readonly Error ConnotPublishArchivedProduct = Error.Validation($"{BaseErrorCode}.{nameof(ConnotPublishArchivedProduct)}");
        public static readonly Error AlreadyUnpublished = Error.Validation($"{BaseErrorCode}.{nameof(AlreadyUnpublished)}");
        public static readonly Error rename4 = Error.Validation($"{BaseErrorCode}.{nameof(rename4)}");
        public static readonly Error CannotPublishThisProductAtLeast1VariantRequired = Error.Validation($"{BaseErrorCode}.{nameof(CannotPublishThisProductAtLeast1VariantRequired)}");
        public static readonly Error rename6 = Error.Validation($"{BaseErrorCode}.{nameof(rename6)}");
        public static readonly Error rename7 = Error.Validation($"{BaseErrorCode}.{nameof(rename7)}");
        public static readonly Error rename8 = Error.Validation($"{BaseErrorCode}.{nameof(rename8)}");
        public static readonly Error rename9 = Error.Validation($"{BaseErrorCode}.{nameof(rename9)}");
        public static readonly Error rename10 = Error.Validation($"{BaseErrorCode}.{nameof(rename10)}");

    }



    public static class ProductVariants
    {
        public const string BaseErrorCode = "DomainErrors.ProductVariants";

        public static readonly Error ProductVariantIdInvalid = Error.Validation($"{BaseErrorCode}.ProductVariantIdInvalid");
        public static readonly Error PriceOutOfRange = Error.Validation($"{BaseErrorCode}.PriceOutOfRange");
        public static readonly Error InvalidDimensions = Error.Validation($"{BaseErrorCode}.InvalidDimensions");

        public static readonly Error BarCodeRequired = Error.Validation($"{BaseErrorCode}.BarCodeRequired");
        public static readonly Error BarCodeLengthOutOfRange = Error.Validation($"{BaseErrorCode}.BarCodeLengthOutOfRange");
        public static readonly Error SlugLengthOutOfRange = Error.Validation($"{BaseErrorCode}.{nameof(SlugLengthOutOfRange)}");

        public static readonly Error ImagesOutOfRange = Error.Validation($"{BaseErrorCode}.ImagesOutOfRange");

        public static readonly Error SkuOutOfRange = Error.Validation($"{BaseErrorCode}.{nameof(SkuOutOfRange)}");
        public static readonly Error AtleastOneSpecificationRequired = Error.Validation($"{BaseErrorCode}.{nameof(AtleastOneSpecificationRequired)}");
        public static readonly Error InvalidSpecification = Error.Validation($"{BaseErrorCode}.{nameof(InvalidSpecification)}");
        public static readonly Error rename4 = Error.Validation($"{BaseErrorCode}.{nameof(rename4)}");
        public static readonly Error rename5 = Error.Validation($"{BaseErrorCode}.{nameof(rename5)}");
        public static readonly Error rename6 = Error.Validation($"{BaseErrorCode}.{nameof(rename6)}");
        public static readonly Error rename7 = Error.Validation($"{BaseErrorCode}.{nameof(rename7)}");
        public static readonly Error rename8 = Error.Validation($"{BaseErrorCode}.{nameof(rename8)}");
        public static readonly Error BarcodeOutOfRange = Error.Validation($"{BaseErrorCode}.{nameof(BarcodeOutOfRange)}");
        public static readonly Error MaxAllowedSpecificationsNumberExceeded = Error.Validation($"{BaseErrorCode}.{nameof(MaxAllowedSpecificationsNumberExceeded)}");

    }





    public static class ProductReviews
    {
        public const string BaseErrorCode = "DomainErrors.ProductReviews";

        public static readonly Error ProductReviewIdInvalid = Error.Validation($"{BaseErrorCode}.ProductReviewIdInvalid");

        public static readonly Error CommentLengthOutOfRange =Error.Validation($"{BaseErrorCode}.CommentLengthOutOfRange");

        public static readonly Error rename1 = Error.Validation($"{BaseErrorCode}.{nameof(rename1)}");
        public static readonly Error rename2 = Error.Validation($"{BaseErrorCode}.{nameof(rename2)}");
        public static readonly Error rename3 = Error.Validation($"{BaseErrorCode}.{nameof(rename3)}");
        public static readonly Error rename4 = Error.Validation($"{BaseErrorCode}.{nameof(rename4)}");
        public static readonly Error rename5 = Error.Validation($"{BaseErrorCode}.{nameof(rename5)}");
        public static readonly Error rename6 = Error.Validation($"{BaseErrorCode}.{nameof(rename6)}");
        public static readonly Error rename7 = Error.Validation($"{BaseErrorCode}.{nameof(rename7)}");
        public static readonly Error rename8 = Error.Validation($"{BaseErrorCode}.{nameof(rename8)}");
        public static readonly Error rename9 = Error.Validation($"{BaseErrorCode}.{nameof(rename9)}");
        public static readonly Error rename10 = Error.Validation($"{BaseErrorCode}.{nameof(rename10)}");
    }




    public static class Orders
    {
        public const string BaseErrorCode = "DomainErrors.Orders";


        public static readonly Error OrderIdInvalid = Error.Validation($"{BaseErrorCode}.OrderIdInvalid");

        public static readonly Error OrderItemsNumberOutOfRange = Error.Validation($"{BaseErrorCode}.OutOfRange");

        public static readonly Error ShippingFeesOutOfRange = Error.Validation($"{BaseErrorCode}.OutOfRange");

        public static readonly Error TotalItemsPriceOutOfRange = Error.Validation($"{BaseErrorCode}.OutOfRange");

        public static readonly Error rename1 = Error.Validation($"{BaseErrorCode}.{nameof(rename1)}");
        public static readonly Error rename2 = Error.Validation($"{BaseErrorCode}.{nameof(rename2)}");
        public static readonly Error rename3 = Error.Validation($"{BaseErrorCode}.{nameof(rename3)}");
        public static readonly Error rename4 = Error.Validation($"{BaseErrorCode}.{nameof(rename4)}");
        public static readonly Error rename5 = Error.Validation($"{BaseErrorCode}.{nameof(rename5)}");
        public static readonly Error rename6 = Error.Validation($"{BaseErrorCode}.{nameof(rename6)}");
        public static readonly Error rename7 = Error.Validation($"{BaseErrorCode}.{nameof(rename7)}");
        public static readonly Error rename8 = Error.Validation($"{BaseErrorCode}.{nameof(rename8)}");
        public static readonly Error rename9 = Error.Validation($"{BaseErrorCode}.{nameof(rename9)}");
        public static readonly Error rename10 = Error.Validation($"{BaseErrorCode}.{nameof(rename10)}");
    }




    
    public static class OrderItems
    {
        public const string BaseErrorCode = "DomainErrors.OrderItems";

        public static readonly Error SerialNumbersDoNotMatchQuantity = Error.Validation($"{BaseErrorCode}.DoNotMatchQuantity");

        public static readonly Error InvalidReturnQuantity = Error.Validation($"{BaseErrorCode}.DoNotMatchQuantity");

        internal static readonly Error OrderItemIdInvalid = Error.Validation($"{BaseErrorCode}.OrderItemIdInvalid");

        public static readonly Error rename1 = Error.Validation($"{BaseErrorCode}.{nameof(rename1)}");
        public static readonly Error rename2 = Error.Validation($"{BaseErrorCode}.{nameof(rename2)}");
        public static readonly Error rename3 = Error.Validation($"{BaseErrorCode}.{nameof(rename3)}");
        public static readonly Error rename4 = Error.Validation($"{BaseErrorCode}.{nameof(rename4)}");
        public static readonly Error rename5 = Error.Validation($"{BaseErrorCode}.{nameof(rename5)}");
        public static readonly Error rename6 = Error.Validation($"{BaseErrorCode}.{nameof(rename6)}");
        public static readonly Error rename7 = Error.Validation($"{BaseErrorCode}.{nameof(rename7)}");
        public static readonly Error rename8 = Error.Validation($"{BaseErrorCode}.{nameof(rename8)}");
        public static readonly Error rename9 = Error.Validation($"{BaseErrorCode}.{nameof(rename9)}");
        public static readonly Error rename10 = Error.Validation($"{BaseErrorCode}.{nameof(rename10)}");
    }




    public static class Brands
    {
        public const string BaseErrorCode = "DomainErrors.Brands";

        public static readonly Error BrandIdInvalid = Error.Validation("DomainErrors.Invalid", "Brand Id is invalid.");

        public static readonly Error rename1 = Error.Validation($"{BaseErrorCode}.{nameof(rename1)}");
        public static readonly Error rename2 = Error.Validation($"{BaseErrorCode}.{nameof(rename2)}");
        public static readonly Error rename3 = Error.Validation($"{BaseErrorCode}.{nameof(rename3)}");
        public static readonly Error rename4 = Error.Validation($"{BaseErrorCode}.{nameof(rename4)}");
        public static readonly Error rename5 = Error.Validation($"{BaseErrorCode}.{nameof(rename5)}");
        public static readonly Error rename6 = Error.Validation($"{BaseErrorCode}.{nameof(rename6)}");
        public static readonly Error rename7 = Error.Validation($"{BaseErrorCode}.{nameof(rename7)}");
        public static readonly Error rename8 = Error.Validation($"{BaseErrorCode}.{nameof(rename8)}");
        public static readonly Error rename9 = Error.Validation($"{BaseErrorCode}.{nameof(rename9)}");
        public static readonly Error rename10 = Error.Validation($"{BaseErrorCode}.{nameof(rename10)}");
    }



    public static class Categories
    {
        public const string BaseErrorCode = "DomainErrors.Categories";

        public static readonly Error CategoryIdInvalid = Error.Validation("DomainErrors.Invalid", "Category Id is invalid.");

        public static readonly Error rename1 = Error.Validation($"{BaseErrorCode}.{nameof(rename1)}");
        public static readonly Error rename2 = Error.Validation($"{BaseErrorCode}.{nameof(rename2)}");
        public static readonly Error rename3 = Error.Validation($"{BaseErrorCode}.{nameof(rename3)}");
        public static readonly Error rename4 = Error.Validation($"{BaseErrorCode}.{nameof(rename4)}");
        public static readonly Error rename5 = Error.Validation($"{BaseErrorCode}.{nameof(rename5)}");
        public static readonly Error rename6 = Error.Validation($"{BaseErrorCode}.{nameof(rename6)}");
        public static readonly Error rename7 = Error.Validation($"{BaseErrorCode}.{nameof(rename7)}");
        public static readonly Error rename8 = Error.Validation($"{BaseErrorCode}.{nameof(rename8)}");
        public static readonly Error rename9 = Error.Validation($"{BaseErrorCode}.{nameof(rename9)}");
        public static readonly Error rename10 = Error.Validation($"{BaseErrorCode}.{nameof(rename10)}");
    }



    public static class CartItems
    {
        public const string BaseErrorCode = "DomainErrors.CartItems";

        public static readonly Error CartItemIdInvalid = Error.Validation($"{BaseErrorCode}.CartItemIdInvalid");
        public static readonly Error QuantityOutOfRange = Error.Validation($"{BaseErrorCode}.QuantityOutOfRange");

        public static readonly Error rename1 = Error.Validation($"{BaseErrorCode}.{nameof(rename1)}");
        public static readonly Error rename2 = Error.Validation($"{BaseErrorCode}.{nameof(rename2)}");
        public static readonly Error rename3 = Error.Validation($"{BaseErrorCode}.{nameof(rename3)}");
        public static readonly Error rename4 = Error.Validation($"{BaseErrorCode}.{nameof(rename4)}");
        public static readonly Error rename5 = Error.Validation($"{BaseErrorCode}.{nameof(rename5)}");
        public static readonly Error rename6 = Error.Validation($"{BaseErrorCode}.{nameof(rename6)}");
        public static readonly Error rename7 = Error.Validation($"{BaseErrorCode}.{nameof(rename7)}");
        public static readonly Error rename8 = Error.Validation($"{BaseErrorCode}.{nameof(rename8)}");
        public static readonly Error rename9 = Error.Validation($"{BaseErrorCode}.{nameof(rename9)}");
        public static readonly Error rename10 = Error.Validation($"{BaseErrorCode}.{nameof(rename10)}");
    }



    public static class Warehouses
    {
        public const string BaseErrorCode = "DomainErrors.Warehouses";

        public static readonly Error WarehouseIdInvalid = Error.Validation($"{BaseErrorCode}.WarehouseIdInvalid");

        public static readonly Error rename1 = Error.Validation($"{BaseErrorCode}.{nameof(rename1)}");
        public static readonly Error rename2 = Error.Validation($"{BaseErrorCode}.{nameof(rename2)}");
        public static readonly Error rename3 = Error.Validation($"{BaseErrorCode}.{nameof(rename3)}");
        public static readonly Error rename4 = Error.Validation($"{BaseErrorCode}.{nameof(rename4)}");
        public static readonly Error rename5 = Error.Validation($"{BaseErrorCode}.{nameof(rename5)}");
        public static readonly Error rename6 = Error.Validation($"{BaseErrorCode}.{nameof(rename6)}");
        public static readonly Error rename7 = Error.Validation($"{BaseErrorCode}.{nameof(rename7)}");
        public static readonly Error rename8 = Error.Validation($"{BaseErrorCode}.{nameof(rename8)}");
        public static readonly Error rename9 = Error.Validation($"{BaseErrorCode}.{nameof(rename9)}");
        public static readonly Error rename10 = Error.Validation($"{BaseErrorCode}.{nameof(rename10)}");

    }



    public static class Customers
    {
        public const string BaseErrorCode = "DomainErrors.Customers";

        public static readonly Error CustomerIdInvalid = Error.Validation($"{BaseErrorCode}.CustomerIdInvalid");



        public static readonly Error rename1 = Error.Validation($"{BaseErrorCode}.{nameof(rename1)}");
        public static readonly Error rename2 = Error.Validation($"{BaseErrorCode}.{nameof(rename2)}");
        public static readonly Error rename3 = Error.Validation($"{BaseErrorCode}.{nameof(rename3)}");
        public static readonly Error rename4 = Error.Validation($"{BaseErrorCode}.{nameof(rename4)}");
        public static readonly Error rename5 = Error.Validation($"{BaseErrorCode}.{nameof(rename5)}");
        public static readonly Error rename6 = Error.Validation($"{BaseErrorCode}.{nameof(rename6)}");
        public static readonly Error rename7 = Error.Validation($"{BaseErrorCode}.{nameof(rename7)}");
        public static readonly Error rename8 = Error.Validation($"{BaseErrorCode}.{nameof(rename8)}");
        public static readonly Error rename9 = Error.Validation($"{BaseErrorCode}.{nameof(rename9)}");
        public static readonly Error rename10 = Error.Validation($"{BaseErrorCode}.{nameof(rename10)}");


    }



    public static class Common
    {
        public const string BaseErrorCode = "DomainErrors.Common";

        public static readonly Error MoneyAmountInvalid = Error.Validation($"{BaseErrorCode}.MoneyAmountInvalid");

        public static readonly Error rename1 = Error.Validation($"{BaseErrorCode}.{nameof(rename1)}");
        public static readonly Error rename2 = Error.Validation($"{BaseErrorCode}.{nameof(rename2)}");
        public static readonly Error rename3 = Error.Validation($"{BaseErrorCode}.{nameof(rename3)}");
        public static readonly Error rename4 = Error.Validation($"{BaseErrorCode}.{nameof(rename4)}");
        public static readonly Error rename5 = Error.Validation($"{BaseErrorCode}.{nameof(rename5)}");
        public static readonly Error rename6 = Error.Validation($"{BaseErrorCode}.{nameof(rename6)}");
        public static readonly Error rename7 = Error.Validation($"{BaseErrorCode}.{nameof(rename7)}");
        public static readonly Error rename8 = Error.Validation($"{BaseErrorCode}.{nameof(rename8)}");
        public static readonly Error rename9 = Error.Validation($"{BaseErrorCode}.{nameof(rename9)}");
        public static readonly Error rename10 = Error.Validation($"{BaseErrorCode}.{nameof(rename10)}");

    }



    public static class CustomerShippingAddresses
    {
        public const string BaseErrorCode = "DomainErrors.CustomerShippingAddresses";

        public static readonly Error CustomerShippingAddressesIdInvalid = Error.Validation($"{BaseErrorCode}.CustomerShippingAddressesIdInvalid");


        public static readonly Error rename1 = Error.Validation($"{BaseErrorCode}.{nameof(rename1)}");
        public static readonly Error rename2 = Error.Validation($"{BaseErrorCode}.{nameof(rename2)}");
        public static readonly Error rename3 = Error.Validation($"{BaseErrorCode}.{nameof(rename3)}");
        public static readonly Error rename4 = Error.Validation($"{BaseErrorCode}.{nameof(rename4)}");
        public static readonly Error rename5 = Error.Validation($"{BaseErrorCode}.{nameof(rename5)}");
        public static readonly Error rename6 = Error.Validation($"{BaseErrorCode}.{nameof(rename6)}");
        public static readonly Error rename7 = Error.Validation($"{BaseErrorCode}.{nameof(rename7)}");
        public static readonly Error rename8 = Error.Validation($"{BaseErrorCode}.{nameof(rename8)}");
        public static readonly Error rename9 = Error.Validation($"{BaseErrorCode}.{nameof(rename9)}");
        public static readonly Error rename10 = Error.Validation($"{BaseErrorCode}.{nameof(rename10)}");

    }



    public static class Addresses
    {
        public const string BaseErrorCode = "DomainErrors.Addresses";
        public static readonly Error AddressIdInvalid = Error.Validation($"{BaseErrorCode}.AddressIdInvalid");


        public static readonly Error rename1 = Error.Validation($"{BaseErrorCode}.{nameof(rename1)}");
        public static readonly Error rename2 = Error.Validation($"{BaseErrorCode}.{nameof(rename2)}");
        public static readonly Error rename3 = Error.Validation($"{BaseErrorCode}.{nameof(rename3)}");
        public static readonly Error rename4 = Error.Validation($"{BaseErrorCode}.{nameof(rename4)}");
        public static readonly Error rename5 = Error.Validation($"{BaseErrorCode}.{nameof(rename5)}");
        public static readonly Error rename6 = Error.Validation($"{BaseErrorCode}.{nameof(rename6)}");
        public static readonly Error rename7 = Error.Validation($"{BaseErrorCode}.{nameof(rename7)}");
        public static readonly Error rename8 = Error.Validation($"{BaseErrorCode}.{nameof(rename8)}");
        public static readonly Error rename9 = Error.Validation($"{BaseErrorCode}.{nameof(rename9)}");
        public static readonly Error rename10 = Error.Validation($"{BaseErrorCode}.{nameof(rename10)}");

    }



    public static class Shipments
    {
        public const string BaseErrorCode = "DomainErrors.Shipments";
        public static readonly Error ShipmentIdInvalid = Error.Validation($"{BaseErrorCode}.ShipmentIdInvalid");


        public static readonly Error rename1 = Error.Validation($"{BaseErrorCode}.{nameof(rename1)}");
        public static readonly Error rename2 = Error.Validation($"{BaseErrorCode}.{nameof(rename2)}");
        public static readonly Error rename3 = Error.Validation($"{BaseErrorCode}.{nameof(rename3)}");
        public static readonly Error rename4 = Error.Validation($"{BaseErrorCode}.{nameof(rename4)}");
        public static readonly Error rename5 = Error.Validation($"{BaseErrorCode}.{nameof(rename5)}");
        public static readonly Error rename6 = Error.Validation($"{BaseErrorCode}.{nameof(rename6)}");
        public static readonly Error rename7 = Error.Validation($"{BaseErrorCode}.{nameof(rename7)}");
        public static readonly Error rename8 = Error.Validation($"{BaseErrorCode}.{nameof(rename8)}");
        public static readonly Error rename9 = Error.Validation($"{BaseErrorCode}.{nameof(rename9)}");
        public static readonly Error rename10 = Error.Validation($"{BaseErrorCode}.{nameof(rename10)}");

    }



    public static class PaymentProviders
    {
        public const string BaseErrorCode = "DomainErrors.PaymentProviders";
        public static readonly Error PaymentProviderIdInvalid = Error.Validation($"{BaseErrorCode}.PaymentProviderIdInvalid");


        public static readonly Error rename1 = Error.Validation($"{BaseErrorCode}.{nameof(rename1)}");
        public static readonly Error rename2 = Error.Validation($"{BaseErrorCode}.{nameof(rename2)}");
        public static readonly Error rename3 = Error.Validation($"{BaseErrorCode}.{nameof(rename3)}");
        public static readonly Error rename4 = Error.Validation($"{BaseErrorCode}.{nameof(rename4)}");
        public static readonly Error rename5 = Error.Validation($"{BaseErrorCode}.{nameof(rename5)}");
        public static readonly Error rename6 = Error.Validation($"{BaseErrorCode}.{nameof(rename6)}");
        public static readonly Error rename7 = Error.Validation($"{BaseErrorCode}.{nameof(rename7)}");
        public static readonly Error rename8 = Error.Validation($"{BaseErrorCode}.{nameof(rename8)}");
        public static readonly Error rename9 = Error.Validation($"{BaseErrorCode}.{nameof(rename9)}");
        public static readonly Error rename10 = Error.Validation($"{BaseErrorCode}.{nameof(rename10)}");

    }

    public static class Transactions
    {
        public const string BaseErrorCode = "DomainErrors.Transactions";
        public static readonly Error TransactionIdInvalid = Error.Validation($"{BaseErrorCode}.TransactionIdInvalid");



        public static readonly Error rename1 = Error.Validation($"{BaseErrorCode}.{nameof(rename1)}");
        public static readonly Error rename2 = Error.Validation($"{BaseErrorCode}.{nameof(rename2)}");
        public static readonly Error rename3 = Error.Validation($"{BaseErrorCode}.{nameof(rename3)}");
        public static readonly Error rename4 = Error.Validation($"{BaseErrorCode}.{nameof(rename4)}");
        public static readonly Error rename5 = Error.Validation($"{BaseErrorCode}.{nameof(rename5)}");
        public static readonly Error rename6 = Error.Validation($"{BaseErrorCode}.{nameof(rename6)}");
        public static readonly Error rename7 = Error.Validation($"{BaseErrorCode}.{nameof(rename7)}");
        public static readonly Error rename8 = Error.Validation($"{BaseErrorCode}.{nameof(rename8)}");
        public static readonly Error rename9 = Error.Validation($"{BaseErrorCode}.{nameof(rename9)}");
        public static readonly Error rename10 = Error.Validation($"{BaseErrorCode}.{nameof(rename10)}");

    }

    public static class UserPaymentMethodLogs
    {
        public const string BaseErrorCode = "DomainErrors.UserPaymentMethodLogs";
        public static readonly Error UserPaymentMethodLogIdInvalid = Error.Validation($"{BaseErrorCode}.UserPaymentMethodLogIdInvalid");


        public static readonly Error rename1 = Error.Validation($"{BaseErrorCode}.{nameof(rename1)}");
        public static readonly Error rename2 = Error.Validation($"{BaseErrorCode}.{nameof(rename2)}");
        public static readonly Error rename3 = Error.Validation($"{BaseErrorCode}.{nameof(rename3)}");
        public static readonly Error rename4 = Error.Validation($"{BaseErrorCode}.{nameof(rename4)}");
        public static readonly Error rename5 = Error.Validation($"{BaseErrorCode}.{nameof(rename5)}");
        public static readonly Error rename6 = Error.Validation($"{BaseErrorCode}.{nameof(rename6)}");
        public static readonly Error rename7 = Error.Validation($"{BaseErrorCode}.{nameof(rename7)}");
        public static readonly Error rename8 = Error.Validation($"{BaseErrorCode}.{nameof(rename8)}");
        public static readonly Error rename9 = Error.Validation($"{BaseErrorCode}.{nameof(rename9)}");
        public static readonly Error rename10 = Error.Validation($"{BaseErrorCode}.{nameof(rename10)}");

    }

    public static class ProductsStock
    {
        public const string BaseErrorCode = "DomainErrors.ProductsStock";
        public static readonly Error ProductStockIdInvalid = Error.Validation($"{BaseErrorCode}.ProductStockIdInvalid");


        public static readonly Error rename1 = Error.Validation($"{BaseErrorCode}.{nameof(rename1)}");
        public static readonly Error rename2 = Error.Validation($"{BaseErrorCode}.{nameof(rename2)}");
        public static readonly Error rename3 = Error.Validation($"{BaseErrorCode}.{nameof(rename3)}");
        public static readonly Error rename4 = Error.Validation($"{BaseErrorCode}.{nameof(rename4)}");
        public static readonly Error rename5 = Error.Validation($"{BaseErrorCode}.{nameof(rename5)}");
        public static readonly Error rename6 = Error.Validation($"{BaseErrorCode}.{nameof(rename6)}");
        public static readonly Error rename7 = Error.Validation($"{BaseErrorCode}.{nameof(rename7)}");
        public static readonly Error rename8 = Error.Validation($"{BaseErrorCode}.{nameof(rename8)}");
        public static readonly Error rename9 = Error.Validation($"{BaseErrorCode}.{nameof(rename9)}");
        public static readonly Error rename10 = Error.Validation($"{BaseErrorCode}.{nameof(rename10)}");

    }
}
