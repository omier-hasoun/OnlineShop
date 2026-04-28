namespace Domain.ReturnItemRequests.ValueObjects;

public sealed class ReturnItemRequestAttachment
{
    private ReturnItemRequestAttachment()
    {
        
    }
    public ReturnItemRequestAttachment(string filePath)
    {
        FileName = filePath;
    }

    public string FileName { get; private init; } = null!;

}
