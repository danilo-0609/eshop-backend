using Catalog.Application.Common;
using ErrorOr;

namespace Catalog.Application.Products.GetProductsByProductType;

public sealed record GetProductsByProductTypeQuery(string ProductType) 
    : IQueryRequest<ErrorOr<IReadOnlyList<ProductResponse>>>;