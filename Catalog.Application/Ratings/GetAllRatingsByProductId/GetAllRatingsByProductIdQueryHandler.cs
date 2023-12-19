using System.Data;
using BuildingBlocks.Application.Queries;
using Catalog.Application.Common;
using Catalog.Application.Ratings.Catalog.Application.Ratings.GetAllRatingsByProductId;
using Dapper;
using ErrorOr;

namespace Catalog.Application.Ratings.GetAllRatingsByProductId;
internal sealed class GetAllRatingsByProductIdQueryHandler 
    : IQueryRequestHandler<GetAllRatingsByProductIdQuery, ErrorOr<IReadOnlyList<RatingResponse>>>
{
    private readonly IDbConnectionFactory _connectionFactory;

    public GetAllRatingsByProductIdQueryHandler(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<ErrorOr<IReadOnlyList<RatingResponse>>> Handle(GetAllRatingsByProductIdQuery request, CancellationToken cancellationToken)
    {
        using IDbConnection dbConnection = _connectionFactory.CreateOpenConnection();

        const string sql = 
            """
            SELECT TOP(100) PERCENT r.RatingId,
                r.Rate,
                r.UserId,
                r.ProductId,
                r.CreatedDateTime,
                r.UpdatedDateTime,
                r.Feedback
            FROM Ratings r
            WHERE r.ProductId = @ProductId
            """;

         List<RatingResponse> response 
            = (await dbConnection
                .QueryAsync<RatingResponse>(sql, new { request.ProductId })).ToList();
        
        if (response.Count == 0) 
        {
            return Error.NotFound("Rating.NotFound", "Ratings in the product with the id provided were not found");
        } 

        return response.AsReadOnly();
    } 
}