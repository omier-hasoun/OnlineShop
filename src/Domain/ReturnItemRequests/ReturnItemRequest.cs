


namespace Domain.ReturnItemRequests;

public sealed class ReturnItemRequest : AggregateRoot<ReturnItemRequestId>, IHasCreationTime
{

    private ReturnItemRequest(ReturnItemRequestId id, OrderItemId orderItemId, ReturnItemRequestReasonType reasonType, string? customerMessage, ReturnItemRequestType type,
        ReturnItemRequestStatus status, short returnedQuantity, IReadOnlyCollection<ReturnItemRequestAttachment> attachments)
        : base(id)
    {
        OrderItemId = orderItemId;
        ReasonType = reasonType;
        CustomerMessage = customerMessage;
        Type = type;
        Status = status;
        ReturnedQuantity = returnedQuantity;
        Attachments = attachments;
    }

    public static Result<ReturnItemRequest> Create(ReturnItemRequestId id, OrderItemId orderItemId, ReturnItemRequestReasonType reasonType, string? customerMessage, ReturnItemRequestType type, short returnedQuantity, IReadOnlyCollection<ReturnItemRequestAttachment> attachments)
    {
        return new ReturnItemRequest(id, orderItemId, reasonType, customerMessage, type, ReturnItemRequestStatus.PendingArrival, returnedQuantity, attachments);
    }
    public OrderItemId OrderItemId { get; private init; }
    public ReturnItemRequestType Type { get; private set; }


    public ReturnItemRequestReasonType ReasonType { get; private set; }
    public string? CustomerMessage { get; private set; }
    public ReturnItemRequestStatus Status { get; private set; }

    public decimal ShippingFees { get; private set; }
    public short ReturnedQuantity { get; private set; }

    public DateTime CreatedAt {  get; set; }

    private List<ReturnItemRequestAttachment> _attachments = [];
    public IReadOnlyCollection<ReturnItemRequestAttachment> Attachments { get { return _attachments.AsReadOnly(); } private set { _attachments = value is null ?[] : value.ToList(); } }

}
