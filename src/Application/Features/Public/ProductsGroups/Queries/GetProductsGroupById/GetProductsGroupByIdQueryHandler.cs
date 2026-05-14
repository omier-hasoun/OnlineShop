using Application.Features.Public.ProductsGroups.Dtos;
using Domain.ProductsGroups.Products;

namespace Application.Features.Public.ProductsGroups.Queries.GetProductsGroupById;

internal sealed class GetProductsGroupByIdQueryHandler(IAppDbContext context) : IRequestHandler<GetProductsGroupByIdQuery, Result<ProductsGroupDto>>
{
    public async Task<Result<ProductsGroupDto>> Handle(GetProductsGroupByIdQuery request, CancellationToken ct)
    {
        ProductsGroupId productId = request.ProductId;

        var query = context.ProductGroups.AsNoTracking()
                                    .Where(x => x.Id == productId && x.Status == ProductsGroupStatus.Published)
                                    .Join(context.Brands, p => p.BrandId, b => b.Id, (product, brand) => new { product, brand })
                                    .Join(context.Categories, x => x.product.CategoryId, category => category.Id, (pb, category) => new { pb, category })
                                    .Select(
                                            x =>
                                            new ProductsGroupDto(
                                               x.pb.product.Id,
                                               x.pb.product.Title,
                                               x.pb.product.Description,
                                               x.pb.product.Attributes.ToDictionary(),
                                               x.pb.brand.Name,
                                               x.category.Name,
                                               x.pb.product.AverageRating,
                                               x.pb.product.Products.Select(x => new ProductDto(
                                                   id: x.Id,
                                                   price: x.Price,
                                                   discountPercentage : x.DiscountPercentage,
                                                   priceBeforeDiscount:x.PriceBeforeDiscount,
                                                   images : x.Images.ToList(),
                                                   slug: x.Slug,
                                                   specifications: x.Specifications.ToDictionary()
                                                   
                                               )).ToList()
                                            )


                                    );

        ProductsGroupDto? productDto = await query.FirstOrDefaultAsync(ct);

        if (productDto is null)
        {
            return ApplicationErrors.NotFound.Product;
        }

        return productDto;

    }
}
