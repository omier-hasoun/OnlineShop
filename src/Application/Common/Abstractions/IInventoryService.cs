using Domain.ProductGroups.Products;

namespace Application.Common.Abstractions;

public interface IInventoryService
{
    Task<IInventoryService> GetAvailability(
        ProductId productVariantId
    );
}
