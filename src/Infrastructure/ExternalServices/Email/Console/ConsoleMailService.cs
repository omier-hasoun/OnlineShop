
using Application.Common.Dtos;
using Microsoft.Extensions.Logging;

namespace Infrastructure.ExternalServices.Email.Console;

internal sealed class ConsoleMailService(ILogger<ConsoleMailService> logger) : INotificationService
{
    public Task NotifyAsync(NotificationRequest request)
    {
        string email =
        @"
            faking Mail operation.

            Sender: {SenderEmail} 
            Recipient: {RecipientEmail} 
         
            Subject: {Subject}

            Body: 
            {Body}

        ";
        logger.LogInformation(email, request.Sender, request.Recipient, request.Subject, request.Body);

        return Task.CompletedTask;
    }
}
