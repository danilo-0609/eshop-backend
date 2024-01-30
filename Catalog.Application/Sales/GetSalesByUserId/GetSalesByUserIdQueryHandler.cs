using System.Data;
using Catalog.Application.Common;
using Catalog.Application.Sales.GetAllSalesByUserId;
using Catalog.Domain.Sales;
using Dapper;
using ErrorOr;

namespace Catalog.Application.Sales.GetSalesByUserId;

internal sealed class GetSalesByUserIdQueryHandler : IQueryRequestHandler<GetSalesByUserIdQuery, ErrorOr<IReadOnlyList<SaleResponse>>>
{
    private readonly IDbConnectionFactory _dbConnectionFactory;
    private readonly AuthorizationService _authorizationService;

    public GetSalesByUserIdQueryHandler(IDbConnectionFactory dbConnectionFactory, AuthorizationService authorizationService)
    {
        _dbConnectionFactory = dbConnectionFactory;
        _authorizationService = authorizationService;
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
            return SalesErrorCodes.NotFound;
        }

        var authorizationService = _authorizationService.IsUserAuthorized(response.First().UserId);
        
        if (authorizationService.IsError && _authorizationService.IsAdmin() is false)
        {
            return SalesErrorCodes.CannotAccessToContent;
        }

        return response.AsReadOnly();
    }
}