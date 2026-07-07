
namespace Infrastructure.Data.Models;

public sealed class OutboxMessage
{
    public long Id { get; init; }

    public string Type { get; init; } = default!;

    public string Content { get; init; } = default!;

    public DateTime OccurredOnUtc { get; init; }

    public DateTime? ProcessedOnUtc { get; private set; }

    public string? Error { get; private set; }

    public void MarkProcessed()
    {
        ProcessedOnUtc = DateTime.UtcNow;
        Error = null;
    }

    public void MarkFailed(string error)
    {
        Error = error;
    }
}
