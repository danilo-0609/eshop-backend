using BuildingBlocks.Domain;

namespace Shopping.Domain.Orders.Events;

public sealed record OrderPayedDomainEvent(
    Guid DomainEventId,
    OrderId OrderId,
    DateTime OcurredOn) : IDomainEvent;