
namespace Domain.PaymentProviders;

public sealed class PaymentProvider : AggregateRoot<PaymentProviderId>
{
    private PaymentProvider(PaymentProviderId id, string brand, string? logoUrl) 
        : base(id)
    {
        Brand = brand;
        LogoUrl = logoUrl;
    }


    // Has no create feature in application, allowed in db, because it needs technical integration with the provider
    //public static Result<PaymentProvider> Create(PaymentProviderId id, string brandName, string companyName, string? logoUrl)
    //{
    //    return new PaymentProvider(id, brandName, companyName, logoUrl, isActive : true);
    //}
    public string Brand { get; private set; }
    public string? LogoUrl { get; private set; }
}
