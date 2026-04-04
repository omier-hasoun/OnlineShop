using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Orders.OrderItems.OrderItemsSerial;

public readonly record struct OrderItemSerialId
{
    public long Value { get; init; }

    public static implicit operator long(OrderItemSerialId orderItemId) => orderItemId.Value;
    public static implicit operator OrderItemSerialId(long value) => new(value);
    public OrderItemSerialId(long value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("OrderItemSerialId is invalid.", nameof(value));
        }
        Value = value;
    }
}
