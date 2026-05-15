
using Application.Features.Management.ProductGroups.Dtos;

namespace Application.Features.Management.ProductGroups.Queries.GetProductById;

internal sealed class GetProductByIdQueryHandler(IAppDbContext context) : IRequestHandler<GetProductByIdQuery, Result<ProductDto>>
{
    public async Task<Result<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken ct)
    {
        var product = await context.Products.FindAsync(request.ParsedProductId);

        if (product is null)
            return ApplicationErrors.NotFound.Product;

        return new ProductDto(
                product.Id,
                product.PriceBeforeDiscount,
                product.Price,
                product.DiscountPercentage,
                product.DiscountExpiresOn,
                product.Status,
                product.Width,
                product.Height,
                product.Length,
                product.Weight,
                product.Sku,
                product.Slug,
                product.BarCode,
                product.Specifications.ToDictionary(),
                [.. product.Images]
            );
    }
}
