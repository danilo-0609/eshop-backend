using BuildingBlocks.Application.Queries;
using ErrorOr;

namespace Catalog.Application.Products.GetAllProductsBySeller;
public sealed record GetAllProductsBySellerQuery(Guid SellerId) 
    : IQueryRequest<ErrorOr<IReadOnlyList<ProductResponse>>>;