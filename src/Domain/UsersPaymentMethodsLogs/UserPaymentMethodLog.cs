namespace Domain.UsersPaymentMethodsLogs;

public sealed class UserPaymentMethodLog : AggregateRoot<UserPaymentMethodLogId>
{
    private UserPaymentMethodLog()
    {
        
    }
    private UserPaymentMethodLog(UserPaymentMethodLogId id, string providerBrandName, string? providerCustomerId, bool isBlacklisted, IReadOnlyDictionary<string, string>? details)
     : base(id)
    {
        ProviderBrandName = providerBrandName;
        ProviderCustomerId = providerCustomerId;
        IsBlacklisted = isBlacklisted;
        _details = details is null ?[] : details.ToDictionary();
   }

    public static Result<UserPaymentMethodLog> Create(UserPaymentMethodLogId id, string providerBrandName, string? providerCustomerId, IReadOnlyDictionary<string, string>? details)
    {
        return new UserPaymentMethodLog(id, providerBrandName, providerCustomerId, isBlacklisted: false, details);
    }

    public string ProviderBrandName { get; private set; } = null!;

    public string? ProviderCustomerId { get; private set; }
    public bool IsBlacklisted { get; private set; }

    Dictionary<string, string> _details = [];
    public IReadOnlyDictionary<string, string> Details { get { return _details.AsReadOnly(); } private set { _details = value.ToDictionary(); } }

}
