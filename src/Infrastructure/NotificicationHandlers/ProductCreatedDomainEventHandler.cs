
using Application.Common.Dtos;
using Domain.ProductsGroups.Events;
using Hangfire;
using MediatR;

namespace Infrastructure.NotificicationHandlers;

internal sealed class ProductCreatedDomainEventHandler(IBackgroundJobClient backgroundJobClient) : INotificationHandler<DomainEventNotification<ProductCreatedDomainEvent>>
{

    public async Task Handle(DomainEventNotification<ProductCreatedDomainEvent> notification, CancellationToken ct)
    {
        var domainEvent = notification.DomainEvent;

        if(domainEvent is null)
        {
            //log
            return;
        }
        
    }
}
