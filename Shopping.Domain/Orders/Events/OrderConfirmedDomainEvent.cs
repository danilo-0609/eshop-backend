using BuildingBlocks.Domain;

namespace Shopping.Domain.Orders.Events;

public sealed record OrderConfirmedDomainEvent(
    Guid DomainEventId,
    OrderId OrderId,
    DateTime OcurredOn) : IDomainEvent;
