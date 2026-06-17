namespace Domain.Services.Models;

public record OrderLinePreview(
    ProductId ProductId,
    string ProductTitle,
    short Quantity,
    Money UnitPrice,
    Money LineTotal
);

