using Catalog.Application.Common;
using ErrorOr;
using MediatR;

namespace Catalog.Application.Products.ModifyProduct.ModifyPrice;

public sealed record ModifyPriceCommand(Guid ProductId, decimal Price) : ICommandRequest<ErrorOr<Unit>>;
