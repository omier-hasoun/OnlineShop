using Domain.Transactions;

namespace Domain.Orders.Payments;

public sealed class OrderPayment : BaseEntity
{


    private OrderPayment()
    {
    }

    public static Result<OrderPayment> Create(TransactionId id, OrderId orderId, string transactionId, string gatewayName, decimal paidAmount)
    {
        return new OrderPayment()
        {
            Id = id,
            OrderId = orderId,
            TransactionId = transactionId,
            GatewayName = gatewayName,
            PaidAmount = paidAmount,
        };
    }

    public TransactionId Id { get; private init; }
    public OrderId OrderId { get; private set; }
    public string TransactionId { get; private set; } = null!;
    public string GatewayName { get; private set; }= null!;
    public decimal PaidAmount { get; private set; }
    public Order? OrderInfo {get; private set;} = null!;
}
