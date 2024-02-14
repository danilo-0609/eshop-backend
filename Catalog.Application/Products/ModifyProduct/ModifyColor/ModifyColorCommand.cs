using Catalog.Application.Common;
using Catalog.Domain.Products.ValueObjects;
using ErrorOr;
using MediatR;

namespace Catalog.Application.Products.ModifyProduct.ModifyColor;

public sealed record ModifyColorCommand(
    Guid ProductId,
    List<string> Colors) : ICommandRequest<ErrorOr<Unit>>;