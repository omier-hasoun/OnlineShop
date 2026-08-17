using Application.Common.Dtos;
using Application.Entities;

namespace Infrastructure.Services.Users;

public sealed class SecurityEmailSender(IEmailService emailService) : IEmailSender<AppUser>
{
    public async Task SendConfirmationLinkAsync(AppUser user, string email, string confirmationLink)
    {

        if (user.EmailConfirmed)
        {
            return;
        }
        string body =
$@"
<a 
  href=""{confirmationLink}""
  style=""
    display:inline-block;
    padding:12px 24px;
    background:#2563eb;
    color:white;
    text-decoration:none;
    border-radius:6px;
  "">
  Confirm Email
</a>
    ";
        await emailService.SendEmailAsync(new EmailMessageRequest("Omier Hasoun", emailService.NoReplyInfoEmail, email, "Confirm your email", body, true));
    }
    public async Task SendPasswordResetLinkAsync(AppUser user, string email, string resetLink)
    {

        string body =
$@"

<p>click here to reset your password</p>

<a 
  href=""{resetLink}""
  style=""
    display:inline-block;
    padding:12px 24px;
    background:#2563eb;
    color:white;
    text-decoration:none;
    border-radius:6px;
  "">
  Reset your password
</a>
    ";
        await emailService.SendEmailAsync(new EmailMessageRequest("Omier Hasoun", emailService.NoReplyInfoEmail, email, "Reset password link", body, true));
    }

    public async Task SendPasswordResetCodeAsync(AppUser user, string email, string resetCode)
    {
        string body =
$@"

Use this code to reset your password.

code: {resetCode}.
";
        await emailService.SendEmailAsync(new EmailMessageRequest("Omier Hasoun", emailService.NoReplyInfoEmail, email, "Reset password code", body, false));
    }

}
