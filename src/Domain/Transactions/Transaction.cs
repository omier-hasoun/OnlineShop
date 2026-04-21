

namespace Domain.Transactions;

public sealed class Transaction : AggregateRoot<TransactionId>, IHasCreationTime
{


    private Transaction(TransactionId id, string externalTransactionId, string paymentProviderName, decimal transferAmount, string senderId,
        string receiverId, TransactionPersonType senderType, TransactionPersonType receiverType, TransactionStatus status, string? notes, DateTime createdAt)
        : base(id)
    {
        ExternalTransactionId = externalTransactionId;
        PaymentProviderName = paymentProviderName;
        TransferAmount = transferAmount;
        SenderId = senderId;
        ReceiverId = receiverId;
        SenderType = senderType;
        ReceiverType = receiverType;
        Status = status;
        Notes = notes;
        CreatedAt = createdAt;
    }

    public static Result<Transaction> Create(TransactionId id, string externalTransactionId, string paymentProviderName, decimal transferAmount, string senderId,
        string receiverId, TransactionPersonType senderType, TransactionPersonType receiverType, TransactionStatus status, string? notes)
    {
        return new Transaction(id, externalTransactionId, paymentProviderName, transferAmount, senderId, receiverId, senderType, receiverType, status, notes, TimeService.UtcNow);
    }

    public string PaymentProviderName { get; private set; } = null!;
    public string? ExternalTransactionId { get; private set; }
    public string SenderId { get; private set; } = null!;
    public TransactionPersonType SenderType { get; private set; }
    public string ReceiverId { get; private set; } = null!;
    public TransactionPersonType ReceiverType { get; private set; }
    public decimal TransferAmount { get; private set; }
    public string? Notes { get; private set; } = null!;
    public TransactionStatus Status { get; private set; }
    public DateTime CreatedAt { get; set; }
}
