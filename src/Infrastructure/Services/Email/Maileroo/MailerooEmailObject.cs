
namespace Infrastructure.Services.Email.Maileroo;

internal sealed record MailerooEmailObject
{
    public string Address { get; set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("Email address cannot be null");

            field = value;
        } } = null!;
    public string? DisplayName { get; set; } = null;

}
