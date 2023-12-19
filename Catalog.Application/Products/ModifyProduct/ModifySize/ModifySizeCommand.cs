using BuildingBlocks.Application.Commands;
using ErrorOr;
using MediatR;

namespace Catalog.Application.Products.ModifyProduct.ModifySize;
public sealed record ModifySizeCommand(Guid ProductId, string Size) : ICommandRequest<ErrorOr<Unit>>;