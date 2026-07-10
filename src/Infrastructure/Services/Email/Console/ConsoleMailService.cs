
using Application.Common.Dtos;
using Application.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services.Email.Console;

internal sealed class ConsoleMailService(ILogger<ConsoleMailService> logger) : IEmailService
{
    public string NoReplyInfoEmail { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }
    public string ServiceEmail { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }

    public Task SendEmailAsync(EmailMessageRequest request)
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
