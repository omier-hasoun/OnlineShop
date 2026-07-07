
using System.Net.Mail;
using System.Text.Json.Serialization;

namespace Domain.Common.ValueObjects;

public sealed record EmailAddress
{
    private EmailAddress()
    {
        
    }

    [JsonConstructor]
    public EmailAddress(string value)
    {
        Value = value;
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
            Value = email.ToLower()
        };
    }

    public override string ToString()
    {
        return Value;
    }
}
