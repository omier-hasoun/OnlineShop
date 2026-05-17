namespace Api.Requests;

public sealed record UpdateProductGroupRequest
{
    public Guid? NewBrandId { get; init; }

    public long? NewCategoryId { get; init; }

    public string? NewTitle { get; init; }

    public string? NewDescription { get; init; }

    public bool? NewIsSerialized { get; init; }

    public Dictionary<string, string>? NewAttributes { get; init; }

    public UpdateProductGroupRequest()
    {
    }
    
}
