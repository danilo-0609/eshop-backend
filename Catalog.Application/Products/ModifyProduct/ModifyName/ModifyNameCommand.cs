using BuildingBlocks.Application.Commands;
using ErrorOr;
using MediatR;

namespace Catalog.Application.Products.ModifyProduct.ModifyName;
public sealed record ModifyNameCommand(Guid ProductId, string Name) : ICommandRequest<ErrorOr<Unit>>;