
using Application.Features.Public.Brands.Queries.Dtos;

namespace Application.Features.Public.Brands.Queries.ListBrands;

public sealed class ListBrandsQueryHandler(IAppDbContext context) : IRequestHandler<ListBrandsQuery, Result<List<BrandListItemDto>>>
{
    public async Task<Result<List<BrandListItemDto>>> Handle(ListBrandsQuery request, CancellationToken ct)
    {
        
        var brands = await context.Brands.AsNoTracking()
                                         .Where(x => x.IsActive)
                                         .Select(x => new BrandListItemDto(x.Id, x.Name))
                                         .ToListAsync(ct);

        return brands;
    }
}
