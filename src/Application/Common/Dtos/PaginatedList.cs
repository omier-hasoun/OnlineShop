namespace Application.Common.Dtos;

public sealed record PaginatedList<T>
{
    public int Page { get; init; }
    public int Size { get; init; }
    public bool HasMore { get; init; }

    public IReadOnlyCollection<T> Items { get; init; } = [];
}
