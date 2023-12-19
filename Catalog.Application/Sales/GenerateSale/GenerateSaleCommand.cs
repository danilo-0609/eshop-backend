
using BuildingBlocks.Application.Commands;
using ErrorOr;

namespace Catalog.Application.Sales;

public sealed record GenerateSaleCommand(
    Guid ProductId,
    int AmountOfProducts,
    decimal UnitPrice,
    Guid UserId) : ICommandRequest<ErrorOr<Guid>>;