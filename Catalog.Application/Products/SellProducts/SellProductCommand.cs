using BuildingBlocks.Application.Commands;
using ErrorOr;
using MediatR;

namespace Catalog.Application.Products.SellProducts;
public sealed record SellProductCommand(Guid ProductId, int AmountOfProducts) : ICommandRequest<ErrorOr<Unit>>;


