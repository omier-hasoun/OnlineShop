
namespace Domain.ReturnItemRequests.Attachments;

public sealed class ReturnItemRequestAttachment : BaseEntity<ReturnItemRequestAttachmentId>
{
    private ReturnItemRequestAttachment(ReturnItemRequestAttachmentId id, ReturnItemRequestId returnItemRequestId, string fileName, int fileSize)
        : base(id)
    {
        ReturnItemRequestId = returnItemRequestId;
        FileName = fileName;
        FileSize = fileSize;
    }

    public static Result<ReturnItemRequestAttachment> Create(ReturnItemRequestAttachmentId id, ReturnItemRequestId returnItemRequestId, string fileName, int fileSize)
    {
        return new ReturnItemRequestAttachment(id, returnItemRequestId, fileName, fileSize);
    }

    public ReturnItemRequestId ReturnItemRequestId { get; private init; }
    public string FileName { get; private init; }
    public int FileSize { get; private init; }
}
