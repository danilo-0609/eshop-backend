using BuildingBlocks.Application.Queries;
using ErrorOr;

namespace Catalog.Application.Products.GetProductsByName;
public sealed record GetProductsByNameQuery(string Name) : IQueryRequest<ErrorOr<IReadOnlyList<ProductResponse>>>;
