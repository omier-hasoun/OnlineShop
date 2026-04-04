using System;
using System.Collections.Generic;
using System.Text;
using Domain.Orders.OrderItems;

namespace Domain.OrderReturnRequests;

public sealed class OrderReturnRequest : BaseEntity, IHasCreationTime, IHasModificationTime, IModificationAudited
{
    private OrderReturnRequest()
    {
        
    }

    public static Result<OrderReturnRequest> Create(OrderReturnRequestId id, OrderItemId orderItemId, UserId requestedBy, OrderReturnRequestReasonType reasonType, string? customerMessage)
    {
        return new OrderReturnRequest()
        {
            Id = id,
            OrderItemId = orderItemId,
            RequestedBy = requestedBy,
            ReasonType = reasonType,
            CreatedAt = DateTime.UtcNow,
            LastModifiedAt = DateTime.UtcNow,
            LastModifiedBy = requestedBy
        };
    }
    public DateTime CreatedAt {  get; set; }
    public DateTime LastModifiedAt { get; set; }
    public UserId LastModifiedBy { get; set; }
    public UserId LastReviewedBy { get; private set; }
    public OrderReturnRequestId Id { get; private set; }
    public OrderItemId OrderItemId { get; private set; }
    public UserId RequestedBy { get; private set; }
    public OrderReturnRequestReasonType ReasonType { get; private set; }
    public string? CustomerMessage { get; private set; }
    public decimal ShippingFees { get; private set; }
    public decimal AdditionalFees { get; private set; }

    public OrderReturnRequestStatus Status { get; private set; } = OrderReturnRequestStatus.Pending;
}
