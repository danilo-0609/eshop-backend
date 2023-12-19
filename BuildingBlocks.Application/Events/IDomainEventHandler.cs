using System;
using BuildingBlocks.Domain;
using MediatR;

namespace BuildingBlocks.Application.Events;
public interface IDomainEventHandler<in TNotification> : INotificationHandler<TNotification>
    where TNotification : IDomainEvent
{        
}
