using BuildingBlocks.Domain;
using Shopping.Domain.Items;

namespace Shopping.Domain.Basket.Events;

public sealed record BasketBuyRequestedDomainEvent(
    Guid DomainEventId,
    BasketId BasketId,
    Guid CustomerId,
    IReadOnlyList<ItemId> ItemIds,
    decimal TotalAmount,
    DateTime OcurredOn) : IDomainEvent;
