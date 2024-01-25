using Catalog.Application.Common;
using ErrorOr;
using MediatR;

namespace Catalog.Application.Products.RemoveProduct;

public sealed record RemoveProductCommand(Guid Id) : ICommandRequest<ErrorOr<Unit>>;

