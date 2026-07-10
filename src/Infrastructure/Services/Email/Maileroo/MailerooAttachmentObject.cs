
namespace Infrastructure.Services.Email.Maileroo;

internal sealed record MailerooAttachmentObject
{
    public string FileName { get; set
    {
        if (string.IsNullOrEmpty(value))
            throw new ArgumentNullException("FileName cannot be null");

        field = value;
    } } = null!;

    public string Content { get; set
    {
        if (string.IsNullOrEmpty(value))
            throw new ArgumentNullException("Content cannot be null");

        field = value;
    }} = null!;

    public string? ContentType { get; set; } = null;

    public bool Inline { get; set; } = true;
}
