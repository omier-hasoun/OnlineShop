
namespace Application.Common.Abstractions;

public interface INotificationService
{
    Task SendEmailAsync(string message);
}
