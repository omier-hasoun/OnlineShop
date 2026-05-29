
using Application.Common.Dtos;

namespace Application.Common.Extensions;

public static class CollectionsExtensions
{
    public static PaginatedList<TResult> ToPaginatedList<TResult>(this ICollection<TResult>? elements, int page, bool hasMore)
    {
        ArgumentNullException.ThrowIfNull(elements);

        return new PaginatedList<TResult>()
        {
            Items = elements is null ? [] : elements.ToList(),
            Page = page,
            Size = elements is null ? 0 : elements.Count,
            HasMore = hasMore

        };
    }


}
