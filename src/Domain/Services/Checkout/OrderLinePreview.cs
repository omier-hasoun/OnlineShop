namespace Domain.Services.Checkout;

public record OrderLinePreview(
    string? ProductThumbnail,
    long ProductId,
    string ProductTitle,
    short Quantity,
    decimal UnitPrice,
    decimal LineTotal
);

