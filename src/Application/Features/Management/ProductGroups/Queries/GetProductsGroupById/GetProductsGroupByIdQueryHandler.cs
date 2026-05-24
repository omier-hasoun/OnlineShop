

using Application.Features.Management.ProductGroups.Dtos;

namespace Application.Features.Management.ProductGroups.Queries.GetProductsGroupById;

internal sealed class GetProductsGroupByIdQueryHandler(IAppDbContext context) : IRequestHandler<GetProductsGroupByIdQuery, Result<ProductGroupDto>>
{
    public async Task<Result<ProductGroupDto>> Handle(GetProductsGroupByIdQuery query, CancellationToken ct)
    {
        var getProductQuery = context.ProductGroups.AsNoTracking()
                                              .AsSingleQuery()
                                              .Where(g => g.Id == query.ProductGroupId)
                                              .Join(
                                                    context.Brands, g => g.BrandId, b => b.Id,
                                                    (g, b) => new { g, b }
                                              )
                                              .Join(
                                                    context.Categories, x => x.g.CategoryId, c => c.Id,
                                                    (gb, c) => new { gb.g, gb.b, c }
                                              )
                                              .Join(
                                                    context.Users, x => x.g.LastModifiedBy, u => u.Id,
                                                    (gbc, u) => new { gbc.g, gbc.b, gbc.c, u }
                                              )
                                              .Select(
                                                  gbc => new ProductGroupDto(gbc.g.Id, gbc.g.Title, gbc.g.Description, gbc.g.Attributes,
                                                            gbc.g.BrandId, gbc.b.Name, gbc.c.Id, gbc.c.Name, gbc.g.AverageRating, gbc.g.LastModifiedAt,
                                                            gbc.g.LastModifiedBy, gbc.u.UserName!,

                                                  gbc.g.Products.Select(p => new ProductListItemDto(
                                                                            p.Id, p.Price, p.HasActiveDiscount, p.DiscountPercentage,
                                                                            p.PriceAfterDiscount, p.DiscountExpiresOn, p.Status, p.Images.FirstOrDefault(),
                                                                            p.StockPerWarehouse.Select(x => new ProductInventoryDto(x.WarehouseId,x.Warehouse.Name, x.Quantity))
                                                                                               .Take(3)
                                                                                               .ToList()
                                                                       ))
                                                                        .ToList())
                                              );


        var groupDto = await getProductQuery.FirstOrDefaultAsync(ct);

        if (groupDto is null)
            return ApplicationErrors.NotFound.ProductGroup;


        return groupDto;
    }
}
