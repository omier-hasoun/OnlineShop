
using Application.Common.Dtos;
using Application.Features.Management.ProductGroups.Dtos;
using Domain.Brands;
using Domain.Categories;
using Domain.Common.ValueObjects;
using Domain.ProductGroups.Products;
using Shared.Helpers;
using static Domain.DomainErrors;

namespace Application.Common.Extensions;

internal static class QueryableExtensions
{
    public static IQueryable<ProductGroup> ApplyDiscountedProductsFilter(
        this IQueryable<ProductGroup> query)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        return query.Where(g =>
            g.Products.Any(p =>
                p.DiscountExpiresOn != null &&
                p.DiscountExpiresOn > today));
    }

    public static IQueryable<ProductGroup> ApplySearchTextFilter(this IQueryable<ProductGroup> query, string? searchText)
    {
        if (string.IsNullOrEmpty(searchText))
            return query;

        searchText = RegexHelper.Normalize(searchText);
        var words = searchText.Split(' ');

        return query.OrderByDescending(x => x.NormalizedTitle == searchText)
                    .ThenByDescending(x => x.Title.Contains(searchText))
                    .ThenByDescending(x => words.Count(w => x.Title.Contains(w)));
    }
    public static IQueryable<ProductGroup> ApplyBrandFilter(this IQueryable<ProductGroup> query, Guid? brandId)
    {
        if (!brandId.HasValue)
            return query;

        var parsedId = new BrandId(brandId.Value);

        return query.Where(group => group.BrandId == parsedId);
    }
    public static IQueryable<ProductGroup> ApplyCategoryFilter(this IQueryable<ProductGroup> query, long? categoryId)
    {
        if (!categoryId.HasValue)
            return query;

        var parsedId = new CategoryId(categoryId.Value);
        return query.Where(group => group.CategoryId == parsedId);
    }

    public static IQueryable<Product> ApplyMaxPriceFilter(this IQueryable<Product> query, int? maxPrice)
    {
        if (!maxPrice.HasValue)
            return query;

        var MoneyMaxPrice = Money.From(maxPrice.Value).Value;

        return query.Where(x => x.Price <= MoneyMaxPrice);
    }

    public static IQueryable<ProductGroup> ApplyStatusesFilter(this IQueryable<ProductGroup> query, List<ProductGroupState>? statuses)
    {
        if (statuses is null || statuses.Count == 0)
            return query;

        return query.Where(x => statuses.Contains(x.Status));
    }

    public static async Task<PaginatedList<TResult>> ToPaginatedListAsync<TResult>(
        this IQueryable<TResult> query,
        int page,
        int size,
        int totalCount, 
        CancellationToken ct)
    {
        int skip = ((page - 1) * size);

        var list = await query.Skip(skip)
                              .Take(size)
                              .ToListAsync(ct);

        return list.ToPaginatedList(page, totalCount);
    }
}
