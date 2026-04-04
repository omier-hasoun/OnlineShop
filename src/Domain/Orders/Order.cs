
using System.ComponentModel.DataAnnotations;
using Domain.Orders.OrderItems;
using Shared.Validators;


namespace Domain.Orders;

public sealed class Order : BaseEntity, IHasCreationTime
{

    private Order()
    {
    }
    public static decimal CalculateTotalItemsPrice(IReadOnlyList<OrderItem> items)
    {
        if(items == null)
            return 0;

        return items.Sum(item => item.TotalPrice);
    }
    public static Result<Order> Create(OrderId id, UserId userId, decimal shippingFees, IReadOnlyList<OrderItem> Items)
    {
        var totalItemsPrice = CalculateTotalItemsPrice(Items);

        var result = Result.ValidateAll(
            () => ValidateOrderItems(Items),
            () => ValidateShippingFees(shippingFees),
            () => ValidateTotalItemsPrice(totalItemsPrice)
            );

        if (result.Failed)
        {
            return result.Errors;
        }


        return new Order()
        {
            Id = id,
            UserId = userId,
            CreatedAt = TimeService.UtcNow,
            ShippingFees = shippingFees,
            TotalItemsPrice = totalItemsPrice,
            Items = Items
        };
    }
    public OrderId Id { get; private init; }
    public UserId UserId { get; private init; }
    public decimal TotalItemsPrice { get; private set; }
    public decimal ShippingFees { get; private set; }
    public DateTime CreatedAt { get; set; }

    public IReadOnlyList<OrderItem> Items { get; private set; }

    private static Result<Success> ValidateShippingFees(decimal shippingFees)
    {
        if(DataValidator.IsOutOfRange(shippingFees, OrderRules.MinShippingFeesValue, OrderRules.MaxShippingFeesValue))
        {
            return OrderErrors.ShippingFeesOutOfRange;
        }

        return Result.Success;
    }
    private static Result<Success> ValidateTotalItemsPrice(decimal totalItemsPrice)
    {
        if (DataValidator.IsOutOfRange(totalItemsPrice, OrderRules.MinTotalItemsPriceValue, OrderRules.MaxTotalItemsPriceValue))
        {
            return OrderErrors.TotalItemsPriceOutOfRange;
        }
        return Result.Success;
    }
    private static Result<Success> ValidateOrderItems(IReadOnlyList<OrderItem> orderItems)
    {
        if (orderItems is null || DataValidator.IsOutOfRange(orderItems.Count, OrderRules.MinOrderItemsCount, OrderRules.MaxOrderItemsCount))
        {
            return OrderErrors.OrderItemsCountOutOfRange;
        }
        return Result.Success;
    }


}
