namespace Application.Common.Dtos;

public sealed record PaginatedList<T>
{
    public static PaginatedList<T> Empty => new()
    {
        HasMore = false,
        Page = 1,
        Size = 0,
        Items = []
    };

    public int Page { get; init; }
    public int Size { get; init; }
    public bool HasMore { get; init; }

    public IReadOnlyCollection<T> Items { get; init; } = [];
}
