namespace Application.Common.ResponseModels;

public sealed record PaginatedList<T>
{
    public int Page { get; init; }
    public int Size { get; init; }
    public int TotalPages { get; init; }
    public int TotalSize { get; init; }

    public IReadOnlyCollection<T>? Items { get; init; }
}
