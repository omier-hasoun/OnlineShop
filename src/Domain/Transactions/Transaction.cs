
namespace Domain.Transactions;

public sealed class Transaction : AggregateRoot<TransactionId>, IHasCreationTime
{


    private Transaction(TransactionId id, string externalTransactionId, string paymentMethod, Money transferAmount,
        TransactionPersonType senderType, TransactionPersonType receiverType, TransactionStatus status, string? notes, DateTime createdAt)
        : base(id)
    {
        ExternalTransactionId = externalTransactionId;
        PaymentMethod = paymentMethod;
        TransferAmount = transferAmount;
        SenderType = senderType;
        ReceiverType = receiverType;
        Status = status;
        Notes = notes;
        CreatedAt = createdAt;
    }

    public static Result<Transaction> Create(TransactionId id, string externalTransactionId, string paymentProviderName, Money transferAmount,
        TransactionPersonType senderType, TransactionPersonType receiverType, TransactionStatus status, string? notes)
    {
        return new Transaction(id, externalTransactionId, paymentProviderName, transferAmount, senderType, receiverType, status, notes, DateTime.UtcNow);
    }

    public string PaymentMethod { get; private set; } = null!;
    public string? ExternalTransactionId { get; private set; }
    public TransactionPersonType SenderType { get; private set; }
    public TransactionPersonType ReceiverType { get; private set; }
    public Money TransferAmount { get; private init; }
    public string? Notes { get; private set; } = null!;
    public TransactionStatus Status { get; private set; }
    public DateTime CreatedAt { get; set; }
    public string? ProviderCustomerId { get; private set; }

    private Dictionary<string, string> _additionalDetails = [];
    public IReadOnlyDictionary<string, string> AdditionalDetails
    { 
        get { return _additionalDetails.AsReadOnly(); }
        private set { _additionalDetails = value is null ? [] : value.ToDictionary(); } 
    }
}
