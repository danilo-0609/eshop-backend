using Catalog.Application.Common;
using Catalog.Domain.Products.ValueObjects;
using ErrorOr;
using MediatR;

namespace Catalog.Application.Products.ModifyProduct.ModifySize;

public sealed record ModifySizeCommand(Guid ProductId, List<string> Sizes) : ICommandRequest<ErrorOr<Unit>>;