using BuildingBlocks.Domain;
using Shopping.Domain.Items;

namespace Shopping.Domain.Orders.Events;

public sealed record OrderPayedDomainEvent(
    Guid DomainEventId,
    OrderId OrderId,
    ItemId ItemId,
    int AmountOfProducts,
    DateTime OcurredOn) : IDomainEvent;