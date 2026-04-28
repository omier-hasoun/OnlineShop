using Domain.Products.ProductVariants;

namespace Application.Common.Abstractions;

public interface IInventoryService
{
    Task<IInventoryService> GetAvailability(
        ProductVariantId productVariantId
    );
}
