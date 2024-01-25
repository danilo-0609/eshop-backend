using Catalog.Application.Common;
using ErrorOr;
using MediatR;

namespace Catalog.Application.Products.ModifyProduct.ModifyInStock;

public sealed record ModifyInStockCommand(Guid ProductId, int InStock) : ICommandRequest<ErrorOr<Unit>>;