
namespace Domain.ReturnItemRequests;

public static class ReturnItemRequestErrors
{
    public static Error SerialNumbersCountNotEqualToQuantity =>
        Error.Validation("ReturnItemRequest.SerialNumbers.InvalidSNCount", "Serial Numbers count doesn't equal quantity.");

    public static Error SerialNumbersRequired =>
        Error.Validation("ReturnItemRequest.SerialNumbers.Required", "Serial Numbers are required.");

    public static Error ProductDoesNotRequireSerialNumbers =>
        Error.Validation("ReturnItemRequest.SerialNumbers.NotRequired", "This product doesn't require Serial Numbers.");
    public static Error InvalidSerialNumbers =>
        Error.Validation("ReturnItemRequest.SerialNumbers.Invalid", ".");
}
