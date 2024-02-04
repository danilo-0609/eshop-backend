using BuildingBlocks.Domain;

namespace Shopping.Domain.Orders.Events;

public sealed record OrderExpiredDomainEvent(
    Guid DomainEventId,
    OrderId OrderId,
    OrderStatus OrderStatus,
    DateTime ExpiredOn,
    DateTime OcurredOn) : IDomainEvent;