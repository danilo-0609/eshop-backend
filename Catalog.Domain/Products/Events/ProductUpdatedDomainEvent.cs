using BuildingBlocks.Domain;

namespace Catalog.Domain.Products.Events;

public sealed record ProductUpdatedDomainEvent(Guid DomainEventId,
    ProductId ProductId,
    string Name,
    Guid SellerId,
    decimal Price,
    int InStock,
    DateTime OcurredOn) : IDomainEvent;
