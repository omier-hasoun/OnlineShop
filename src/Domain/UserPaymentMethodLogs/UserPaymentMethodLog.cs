
using Domain.Customers;

namespace Domain.UserPaymentMethodLogs;

public sealed class UserPaymentMethodLog : AggregateRoot<UserPaymentMethodLogId>
{
    private UserPaymentMethodLog(UserPaymentMethodLogId id, UserId userId, string providerBrandName, string? providerCustomerId, bool isBlacklisted, IReadOnlyDictionary<string, string>? details)
     : base(id)
    {
        UserId = userId;
        ProviderBrandName = providerBrandName;
        ProviderCustomerId = providerCustomerId;
        IsBlacklisted = isBlacklisted;
        _details = details is null ?[] : details.ToDictionary();
   }

    public static Result<UserPaymentMethodLog> Create(UserPaymentMethodLogId id, UserId userId, string providerBrandName, string? providerCustomerId, IReadOnlyDictionary<string, string>? details)
    {
        return new UserPaymentMethodLog(id, userId, providerBrandName, providerCustomerId, isBlacklisted: false, details);
    }

    public UserId UserId { get; private init; }

    public string ProviderBrandName { get; private set; }

    public string? ProviderCustomerId { get; private set; }
    public bool IsBlacklisted { get; private set; }

    Dictionary<string, string> _details = [];
    public IReadOnlyDictionary<string, string> Details { get { return _details.AsReadOnly(); } private set { _details = value.ToDictionary(); } }

}
