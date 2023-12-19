using BuildingBlocks.Domain;
using Catalog.Domain.Products.Rules;

namespace Catalog.Domain.Products.Events;

public sealed record ProductPublishedDomainEvent(Guid DomainEventId,
    ProductId ProductId,
    Guid SellerId,
    string Name,
    string Description,
    decimal Price,
    int InStock,
    string Size,
    List<Tag> Tags,
    ProductType ProductType,
    DateTime OcurredOn,
    string Color = "") : IDomainEvent;

