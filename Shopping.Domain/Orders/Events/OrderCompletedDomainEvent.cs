using BuildingBlocks.Domain;

namespace Shopping.Domain.Orders.Events;

public sealed record OrderCompletedDomainEvent(
    Guid DomainEventId,
    OrderId OrderId,
    Guid CustomerId,
    DateTime OcurredOn) : IDomainEvent;
