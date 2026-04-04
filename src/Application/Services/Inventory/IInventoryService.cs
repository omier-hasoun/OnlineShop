using Domain.Products.ProductVariants;

namespace Application.Services.Inventory;

public interface IInventoryService
{
    Task<IInventoryService> GetAvailability(
        ProductVariantId productVariantId
    );
}
