using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.OrderReturnRequests;

public readonly record struct OrderReturnRequestId
{
    public Guid Value { get; init; }

    public static implicit operator Guid(OrderReturnRequestId orderReturnRequestId) => orderReturnRequestId.Value;
    public static implicit operator OrderReturnRequestId(Guid value) => new OrderReturnRequestId(value);
    public OrderReturnRequestId(Guid value)
    {
        if (value.Version != 7 || value == default)
            throw new ArgumentException("orderReturnRequestId is invalid.", nameof(value));

        Value = value;
    }
}
