using Catalog.Application.Common;
using ErrorOr;
using MediatR;

namespace Catalog.Application.Products.ModifyProduct.ModifyDescription;

public sealed record ModifyDescriptionCommand(Guid ProductId, string Description) : ICommandRequest<ErrorOr<Unit>>;