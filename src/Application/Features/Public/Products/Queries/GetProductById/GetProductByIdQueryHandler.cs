using Application.Features.Public.Products.Dtos;

namespace Application.Features.Public.Products.Queries.GetProductById;

internal sealed class GetProductByIdQueryHandler(IAppDbContext context) : IRequestHandler<GetProductByIdQuery, Result<ProductDto>>
{
    public async Task<Result<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken ct)
    {
        ProductId productId = new(request.ProductId);

        var query = context.Products.AsNoTracking()
                                    .Where(x => x.Id == productId && x.Status == ProductStatus.Published)
                                    .Join(context.Brands, p => p.BrandId, b => b.Id, (product, brand) => new { product, brand })
                                    .Join(context.Categories, x => x.product.CategoryId, category => category.Id, (pb, category) => new { pb, category })
                                    .Select(
                                            x =>
                                            new ProductDto(
                                               x.pb.product.Title,
                                               x.pb.product.Description,
                                               x.pb.product.Attributes.ToDictionary(),
                                               x.pb.brand.Name,
                                               x.category.Name,
                                               x.pb.product.AverageRating,
                                               x.pb.product.Variants.Select(x => new ProductVariantDto(
                                               
                                                   price: x.Price,
                                                   discountPercentage : x.DiscountPercentage,
                                                   priceBeforeDiscount:x.PriceBeforeDiscount,
                                                   images : x.Images.ToList(),
                                                   slug: x.Slug,
                                                   specifications: x.Specifications.ToDictionary()
                                                   
                                               )).ToList()
                                            )


                                    );

        ProductDto? productDto = await query.FirstOrDefaultAsync(ct);

        if (productDto is null)
        {
            return ApplicationErrors.NotFound.Product;
        }

        return productDto;

    }
}
