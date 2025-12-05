
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Presentation.Notification;

public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        // Implement your email sending logic here.
        Console.WriteLine($"Sending email to {email} with subject '{subject}'");
        Console.WriteLine($"Message: {htmlMessage}");
        return Task.CompletedTask;
    }
}
