
using Application.Common.Dtos;

namespace Application.Common.Extensions;

public static class CollectionsExtensions
{
    public static PaginatedList<TResult> ToPaginatedList<TResult>(this ICollection<TResult> elements, int pageNumber, int totalCount)
    {
        ArgumentNullException.ThrowIfNull(elements);

        return new PaginatedList<TResult>()
        {
            Items = elements.ToList(),
            Page = pageNumber,
            Size = elements.Count,
            TotalSize = totalCount,
            TotalPages = elements.Count == 0 ? 1 : (int)Math.Ceiling(((decimal)(decimal)totalCount / (decimal)elements.Count ))

        };
    }


}
