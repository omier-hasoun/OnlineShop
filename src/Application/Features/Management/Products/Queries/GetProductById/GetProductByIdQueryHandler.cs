

using Application.Features.Management.Products.Dtos;

namespace Application.Features.Management.Products.Queries.GetProductById;

internal sealed class GetProductByIdQueryHandler(IAppDbContext context) : IRequestHandler<GetProductByIdQuery, Result<ProductDto>>
{
    public async Task<Result<ProductDto>> Handle(GetProductByIdQuery query, CancellationToken ct)
    {

        var productId = query.ProductId;

        var getProductQuery = context.Products.AsNoTracking()
                                              .Include(x => x.Variants)
                                              .Where(p => p.Id == productId)
                                              .Join(
                                                    context.Brands, p => p.BrandId, b => b.Id,
                                                    (p, b) => new { p, b }
                                              )
                                              .Join(
                                                    context.Categories, x => x.p.CategoryId, c => c.Id,
                                                    (pb, c) => new { pb, c }
                                              )
                                              .Select(
                                                  x => new ProductDto(x.pb.p.Id, x.pb.p.Title, x.pb.p.Description, x.pb.p.Attributes.ToDictionary()
                                                  , x.pb.p.BrandId, x.pb.b.Name, x.c.Id, x.c.Name, x.pb.p.AverageRating,

                                                  x.pb.p.Variants.Select(v => new ProductVariantDto(v.Id, v.Price, v.DiscountPercentage, v.PriceBeforeDiscount, v.Images.ToList(),
                                                  v.Slug, v.Specifications.ToDictionary())).ToList())

                                              );


        var product = await getProductQuery.FirstOrDefaultAsync(ct);

        if (product is null)
            return ApplicationErrors.NotFound.Product;


        return product;
    }
}
