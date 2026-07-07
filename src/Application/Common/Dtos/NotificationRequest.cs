
namespace Application.Common.Dtos;

public sealed record NotificationRequest
(
    string Sender,
    string Recipient,
    string Subject,
    string Body
);
