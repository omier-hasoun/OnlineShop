

using Application.Features.Management.ProductGroups.Dtos;

namespace Application.Features.Management.ProductGroups.Queries.GetProductsGroupById;

internal sealed class GetProductsGroupByIdQueryHandler(IAppDbContext context) : IRequestHandler<GetProductsGroupByIdQuery, Result<ProductsGroupDto>>
{
    public async Task<Result<ProductsGroupDto>> Handle(GetProductsGroupByIdQuery query, CancellationToken ct)
    {

        var productId = query.ProductId;

        var getProductQuery = context.ProductGroups.AsNoTracking()
                                              .Include(x => x.Products)
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
                                                  x => new ProductsGroupDto(x.pb.p.Id, x.pb.p.Title, x.pb.p.Description, x.pb.p.Attributes.ToDictionary()
                                                  , x.pb.p.BrandId, x.pb.b.Name, x.c.Id, x.c.Name, x.pb.p.AverageRating,

                                                  x.pb.p.Products.Select(v => new ProductDto(v.Id, v.Price, v.DiscountPercentage, v.PriceBeforeDiscount, v.Images.ToList(),
                                                  v.Slug, v.Specifications.ToDictionary())).ToList())

                                              );


        var productGroup = await getProductQuery.FirstOrDefaultAsync(ct);

        if (productGroup is null)
            return ApplicationErrors.NotFound.ProductGroup;


        return productGroup;
    }
}
