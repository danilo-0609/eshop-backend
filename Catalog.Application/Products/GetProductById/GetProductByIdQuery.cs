using Catalog.Application.Common;
using ErrorOr;

namespace Catalog.Application.Products.GetProductById;

public sealed record GetProductByIdQuery(Guid ProductId) : IQueryRequest<ErrorOr<ProductResponse>>;
