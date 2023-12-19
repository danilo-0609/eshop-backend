using System.Data;
using BuildingBlocks.Application.Queries;
using Catalog.Application.Common;
using Catalog.Application.Sales.GetAllSalesByUserId;
using Dapper;
using ErrorOr;

namespace Catalog.Application.Sales.GetSalesByUserId;

internal sealed class GetSalesByUserIdQueryHandler 
    : IQueryRequestHandler<GetSalesByUserIdQuery, ErrorOr<IReadOnlyList<SaleResponse>>>
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public GetSalesByUserIdQueryHandler(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<ErrorOr<IReadOnlyList<SaleResponse>>> Handle(GetSalesByUserIdQuery query, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _dbConnectionFactory.CreateOpenConnection();
    
        const string sql = 
            """"
            SELECT TOP(100) PERCENT   
                o.SaleId,
                o.ProductId,
                o.AmountOfProducts,
                o.Total,
                o.UserId,
                o.CreatedDateTime 
            FROM Sales o
            WHERE o.UserId = @UserId
            """";

        List<SaleResponse> response 
            = (await connection
                .QueryAsync<SaleResponse>(sql, new { query.UserId })).ToList();

        if (response.Count == 0)
        {
            return Error.NotFound("Sales.NotFound", "Sales with the user id provided were not found");
        }

        return response.AsReadOnly();
    }
}