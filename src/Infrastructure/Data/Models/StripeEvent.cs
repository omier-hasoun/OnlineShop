namespace Infrastructure.Data.Models;

public enum StripeEventState
{
    Pending = 1,
    Processing = 2,
    Processed = 3,
    Failed = 4
}

public sealed class StripeEvent
{
    public long Id { get; set; }
    public string Type { get; set; } = null!;
    public string StripeSessionId { get; set; } = null!;
    public StripeEventState Status { get; set; }
    public string StripeEventId { get; set; } = null!;
    public DateTime ReceivedAt { get; set; }
    public DateTime? ProcessedAt { get; set; }

}
