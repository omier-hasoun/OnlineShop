
using System.Net.Mail;

namespace Domain.Common.ValueObjects;

public sealed record EmailAddress
{
    private EmailAddress()
    {
        
    }

    public string Value { get; private init; } = null!;

    public static Result<EmailAddress> Create(string email)
    {
        try
        {
            _ = new MailAddress(email);
        }
        catch
        {
            return DomainErrors.EmailInvalid;
        }

        return new EmailAddress
        {
            Value = RegexHelper.Normalize(email)
        };
    }
}
