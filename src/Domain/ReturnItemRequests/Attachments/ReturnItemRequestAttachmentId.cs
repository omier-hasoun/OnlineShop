
namespace Domain.ReturnItemRequests.Attachments;

public readonly record struct ReturnItemRequestAttachmentId
{
    public Guid Value { get; init; }

    public static implicit operator Guid(ReturnItemRequestAttachmentId requestAttachmentId) => requestAttachmentId.Value;
    public static implicit operator ReturnItemRequestAttachmentId(Guid value) => new ReturnItemRequestAttachmentId(value);
    public ReturnItemRequestAttachmentId(Guid value)
    {
        if (value.Version != 7 || value == default)
            throw new ArgumentException("RequestAttachmentId is invalid.", nameof(value));

        Value = value;
    }
}
