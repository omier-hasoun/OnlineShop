
using Application.Common.Dtos;

namespace Application.Common.Abstractions;

public interface IEmailService
{
    public string NoReplyInfoEmail { get; init; }
    public string ServiceEmail { get; init; }

    Task SendEmailAsync(EmailMessageRequest request);
}
