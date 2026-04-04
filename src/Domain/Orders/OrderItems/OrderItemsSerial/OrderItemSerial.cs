using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Orders.OrderItems.OrderItemsSerial;

public sealed class OrderItemSerial : BaseEntity
{
    private OrderItemSerial()
    {
    }
    public static Result<OrderItemSerial> Create(OrderItemSerialId id, OrderItemId orderItemId, string serialNumber)
    {
        return new OrderItemSerial()
        {
            Id = id,
            OrderItemId = orderItemId,
            SerialNumber = serialNumber,
        };
    }
    public OrderItemSerialId Id { get; private init; }
    public OrderItemId OrderItemId { get; private init; }
    public string SerialNumber
    {
        get;
        private set
        {
            DomainInvariantsException.ThrowIfStringLengthOutOfRange(value, nameof(SerialNumber), OrderItemSerialRules.MinSerialNumberLength, OrderItemSerialRules.MaxSerialNumberLength);
            field = value;
        }
    } = null!;
}
