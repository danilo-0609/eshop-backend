using Catalog.Application.Common;
using ErrorOr;
using MediatR;

namespace Catalog.Application.Products.ModifyProduct.ModifyProductType;

public sealed record ModifyProductTypeCommand(Guid ProductId, string ProductType) : ICommandRequest<ErrorOr<Unit>>;