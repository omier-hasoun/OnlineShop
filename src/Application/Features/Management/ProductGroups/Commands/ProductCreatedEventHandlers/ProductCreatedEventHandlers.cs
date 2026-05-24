
using Application.Common.Dtos;
using Domain.ProductGroups.Events;
using Microsoft.Extensions.Logging;

namespace Application.Features.Management.ProductGroups.Commands.ProductCreatedEventHandlers;


internal sealed class ProductCreatedEventHandlers(ILogger<ProductCreatedEventHandlers> logger, TimeProvider provider) : INotificationHandler<DomainEventNotification<ProductCreatedDomainEvent>>
{

    public Task Handle(
        DomainEventNotification<ProductCreatedDomainEvent> notification,
        CancellationToken cancellationToken)
    {
        var productId = notification.DomainEvent.ProductId;

        if (logger.IsEnabled(LogLevel.Information))
        {
            var time = provider.GetUtcNow();

            logger.LogInformation(
                "Product {ProductId} created at {Time}",
                productId,
                time);
        }

        return Task.CompletedTask;
    }
}
