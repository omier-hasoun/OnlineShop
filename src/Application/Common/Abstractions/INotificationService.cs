
using Application.Common.Dtos;

namespace Application.Common.Abstractions;

public interface INotificationService
{
    Task NotifyAsync(NotificationRequest request);
}
