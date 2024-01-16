using BuildingBlocks.Domain;

namespace Catalog.Domain.Products.Events;

public sealed record ProductSellFailedDomainEvent(
    Guid DomainEventId,
    ProductId ProductId,
    Guid OrderId,
    string FailureCause,
    DateTime OcurredOn) : IDomainEvent;
