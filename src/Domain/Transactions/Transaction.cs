using Domain.Common.Enums;

namespace Domain.Transactions;

public sealed class Transaction : BaseEntity
{


    private Transaction()
    {
    }

    public static Result<Transaction> Create(TransactionId id, OrderId orderId, string transactionId, string gatewayName, decimal paidAmount)
    {
        return new Transaction()
        {
            Id = id,
            ExternalTransactionId = transactionId,
            PaymentProviderName = gatewayName,
            TransferAmount = paidAmount,
        };
    }

    public TransactionId Id { get; private init; }
    public string PaymentProviderName { get; private set; } = null!;
    public string? ExternalTransactionId { get; private set; }
    public string SenderId { get; private set; }= null!;
    public TransactorPersonType SenderType { get; private set; }
    public string ReceiverId {get; private set;} = null!;
    public TransactorPersonType ReceiverType { get; private set; }
    public string ReasonType { get; private set; } = null!;
    public string? Reason { get; private set; } = null!;
    public string? Notes { get; private set; } = null!;

    public decimal TransferAmount { get; private set; }
    public CurrencyCode Currency { get; private set; } = CurrencyCode.USD;





}
