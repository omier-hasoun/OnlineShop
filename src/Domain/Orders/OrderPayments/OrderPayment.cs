
namespace Domain.Orders.OrderPayments;

public sealed class OrderPayment : BaseEntity<TransactionId>
{


    private OrderPayment(TransactionId id, OrderId orderId, UserPaymentMethodLogId paymentMethodLogId, string invoiceFileName) : base(id)
    {
        TransactionId = id;
        OrderId = orderId;
        PaymentMethodLogId = paymentMethodLogId;
        InvoiceFileName = invoiceFileName;
    }

    public static Result<OrderPayment> Create(TransactionId id, OrderId orderId, UserPaymentMethodLogId paymentMethodLogId, string invoiceFileNam)
    {
        return new OrderPayment(id, orderId, paymentMethodLogId, invoiceFileNam);
    }

    public TransactionId TransactionId { get; private init; }
    public OrderId OrderId { get; private init; }
    public UserPaymentMethodLogId PaymentMethodLogId { get; private init; }
    public string InvoiceFileName { get; private set; } = null!;

}
