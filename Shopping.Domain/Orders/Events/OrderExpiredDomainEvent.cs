using BuildingBlocks.Domain;

namespace Shopping.Domain.Orders.Events;

public sealed record OrderExpiredDomainEvent(
    Guid DomainEventId,
    OrderId OrderId,
    DateTime OcurredOn) : IDomainEvent;