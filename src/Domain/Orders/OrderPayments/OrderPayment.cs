
namespace Domain.Orders.OrderPayments;

public sealed class OrderPayment : BaseEntity<TransactionId>
{


    private OrderPayment(TransactionId id, OrderId orderId, UserPaymentMethodLogId userPaymentMethodLogId, string invoiceFileName) : base(id)
    {
        OrderId = orderId;
        UserPaymentMethodLogId = userPaymentMethodLogId;
        InvoiceFileName = invoiceFileName;
    }

    public static Result<OrderPayment> Create(TransactionId id, OrderId orderId, UserPaymentMethodLogId userPaymentMethodLogId, string invoiceFileNam)
    {
        return new OrderPayment(id, orderId, userPaymentMethodLogId, invoiceFileNam);
    }

    public TransactionId TransactionId { get { return Id; } }
    public OrderId OrderId { get; private init; }
    public UserPaymentMethodLogId UserPaymentMethodLogId { get; private init; }
    public string InvoiceFileName { get; private set; } = null!;

}
