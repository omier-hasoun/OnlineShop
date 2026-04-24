


using Domain.Common.ValueObjects;

namespace Domain.ReturnItemRequests;

public sealed class ReturnItemRequest : AggregateRoot<ReturnItemRequestId>, IHasCreationTime
{
    private ReturnItemRequest()
    {
        
    }

    private ReturnItemRequest(ReturnItemRequestId id, OrderItemId orderItemId, ReturnItemRequestReasonType reasonType, string? customerMessage, ReturnItemRequestType type,
        ReturnItemRequestStatus status, short returnedQuantity)
        : base(id)
    {
        OrderItemId = orderItemId;
        ReasonType = reasonType;
        CustomerMessage = customerMessage;
        Type = type;
        Status = status;
        ReturnedQuantity = returnedQuantity;
    }

    public static Result<ReturnItemRequest> Create(ReturnItemRequestId id, OrderItemId orderItemId, ReturnItemRequestReasonType reasonType, string? customerMessage, ReturnItemRequestType type, short returnedQuantity)
    {
        return new ReturnItemRequest(id, orderItemId, reasonType, customerMessage, type, ReturnItemRequestStatus.PendingArrival, returnedQuantity);
    }
    public OrderItemId OrderItemId { get; private init; }
    public ReturnItemRequestType Type { get; private set; }


    public ReturnItemRequestReasonType ReasonType { get; private set; }
    public string? CustomerMessage { get; private set; }
    public ReturnItemRequestStatus Status { get; private set; }

    public Money ShippingFees { get; }
    public short ReturnedQuantity { get; private set; }

    public DateTime CreatedAt {  get; set; }

    private List<ReturnItemRequestAttachment> _attachments = [];
    public IReadOnlyCollection<ReturnItemRequestAttachment> Attachments { get { return _attachments.AsReadOnly(); } private set { _attachments = value is null ?[] : value.ToList(); } }

}
