using System;
using System.Collections.Generic;
using System.Text;
using Domain.Addresses;

namespace Domain.Orders.OrderItems;

public readonly record struct OrderItemId
{
    public long Value { get; init; }

    public static implicit operator long(OrderItemId orderItemId) => orderItemId.Value;
    public static implicit operator OrderItemId(long value) => new(value);
    public OrderItemId(long value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("OrderItemId is invalid.", nameof(value));
        }
        Value = value;
    }
}
