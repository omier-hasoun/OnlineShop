namespace Api.Requests;

public sealed record UpdateProductGroupRequest
{
    public Guid? New_Brand_Id { get; init; }

    public long? New_Category_Id { get; init; }

    public string? New_Title { get; init; }

    public string? New_Description { get; init; }

    public bool? New_Is_Serialized { get; init; }

    public Dictionary<string, string>? New_Attributes { get; init; }

    public UpdateProductGroupRequest()
    {
    }
    
}
