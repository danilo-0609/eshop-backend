using BuildingBlocks.Domain;
using Shopping.Domain.Items;

namespace Shopping.Domain.Orders.Events;

public sealed record OrderPlacedDomainEvent(
    Guid DomainEventId,
    OrderId OrderId,
    Guid CustomerId,
    ItemId ItemId,
    OrderStatus OrderStatus,
    int AmountRequested,
    decimal TotalMoneyAmount,
    DateTime OcurredOn) : IDomainEvent;
