


using Application.Common.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Infrastructure.BackgroundServices;

public sealed class EmailSenderFaker: IEmailSender<AppUser>, IEmailSender
{
    public Task SendConfirmationLinkAsync(AppUser user, string email, string confirmationLink)
    {

        if(user.EmailConfirmed)
        {
            Console.WriteLine("Email is already confirmed.");
        }
        Console.WriteLine($"Send confirmation: {confirmationLink}");

        return Task.CompletedTask;
    }

    public Task SendPasswordResetLinkAsync(AppUser user, string email, string resetLink)
    {

        Console.WriteLine(@$"/////Send password reset: {resetLink}\\\\\\");

        return Task.CompletedTask;
    }

    public Task SendPasswordResetCodeAsync(AppUser user, string email, string resetCode)
    {
        Console.WriteLine($"Reset code: {resetCode}");
        return Task.CompletedTask;
    }

    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        // Implement your email sending logic here.
        Console.WriteLine($"Sending email to {email} with subject '{subject}'");
        Console.WriteLine($"Message: {htmlMessage}");
        return Task.CompletedTask;
    }
}
