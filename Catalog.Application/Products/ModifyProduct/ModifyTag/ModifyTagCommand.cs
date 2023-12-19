using BuildingBlocks.Application.Commands;
using ErrorOr;
using MediatR;

namespace Catalog.Application.Products.ModifyProduct.ModifyTag;
public sealed record ModifyTagCommand(Guid ProductId, List<string> Tags) : ICommandRequest<ErrorOr<Unit>>;