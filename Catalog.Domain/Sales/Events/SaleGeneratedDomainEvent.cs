using BuildingBlocks.Domain;
using Catalog.Domain.Products;

namespace Catalog.Domain.Sales.Events;

public sealed record SaleGeneratedDomainEvent(
    Guid DomainEventId,
    ProductId ProductId,
    int AmountOfProductsSold,
    Guid UserId,
    DateTime OcurredOn) : IDomainEvent;
