using BuildingBlocks.Application.Queries;
using ErrorOr;

namespace Catalog.Application.Products.GetProductsByTag;
public sealed record GetProductsByTagQuery(string Tag) 
    : IQueryRequest<ErrorOr<IReadOnlyList<ProductResponse>>>;