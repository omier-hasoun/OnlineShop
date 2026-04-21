
namespace Domain.PaymentProviders;

public sealed class PaymentProvider : AggregateRoot<PaymentProviderId>
{
    private PaymentProvider(PaymentProviderId id, string brandName, string companyName, string? logoUrl, bool isActive) 
        : base(id)
    {
        BrandName = brandName;
        CompanyName = companyName;
        LogoUrl = logoUrl;
        IsActive = isActive;
    }


    // Has no create feature in application, allowed in db, because it needs technical integration with the provider
    //public static Result<PaymentProvider> Create(PaymentProviderId id, string brandName, string companyName, string? logoUrl)
    //{
    //    return new PaymentProvider(id, brandName, companyName, logoUrl, isActive : true);
    //}
    public string BrandName { get; private set; }
    public string CompanyName { get; private set; }
    public string? LogoUrl { get; private set; }
    public bool IsActive { get; private set; }

    public Result<Updated> Activate()
    {
        IsActive = true;
        return Result.Updated;
    }

    public Result<Updated> Deactivate()
    {
        IsActive = false;
        return Result.Updated;
    }
}
