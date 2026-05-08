
namespace Domain.Orders.ValueObjects;

public sealed record ProductInfoSnapShotAtPurchase
{
    public string productId { get; init; } = null!;
    public string Description { get; } = null!;
    public string Title { get; init; } = null!;
    public Dictionary<string, string> Attributes { get; init; } = null!;
    public float Price { get; init; }
    public Dictionary<string, string> VariantSpecification { get; init; } = null!;



    
}
