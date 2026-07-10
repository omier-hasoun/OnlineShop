
namespace Application.Common.Dtos;

public sealed record EmailMessageRequest
(
    string SenderDisplayName,
    string Sender,
    string Recipient,
    string Subject,
    string Body,
    bool IsHtml = false
);
