using BuildingBlocks.Application.Commands;
using ErrorOr;
using MediatR;

namespace Catalog.Application.Products.ModifyProduct.ModifyColor;

public sealed record ModifyColorCommand(
    Guid ProductId,
    string Color) : ICommandRequest<ErrorOr<Unit>>;