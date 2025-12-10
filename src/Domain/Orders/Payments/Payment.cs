namespace Domain.Orders.Payments;

public sealed class Payment : BaseEntity
{


    private Payment()
    {
    }

    public static Payment Create(OrderId orderId, string transactionId, string gatewayName, decimal paidAmount)
    {
        return new()
        {
            TransactionId = transactionId,
            GatewayName = gatewayName,
            OrderId = orderId,
            PaidAmount = paidAmount,
        };
    }

    public OrderId OrderId { get; private init; }
    public string TransactionId { get; private set; } = null!;
    public string GatewayName { get; private set; }= null!;
    public decimal PaidAmount { get; private set; }
    public Order? OrderInfo {get; private set;} = null!;
}
