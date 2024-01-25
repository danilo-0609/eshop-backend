using Catalog.Application.Common;
using ErrorOr;
using MediatR;

namespace Catalog.Application.Products.SellProducts;
public sealed record SellProductCommand(
    Guid ProductId, 
    Guid OrderId,
    int AmountOfProducts) : ICommandRequest<ErrorOr<Unit>>;


