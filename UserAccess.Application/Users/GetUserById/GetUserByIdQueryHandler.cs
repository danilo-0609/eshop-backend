using System.Data;
using BuildingBlocks.Application.Queries;
using Dapper;
using ErrorOr;
using UserAccess.Application.Common;

namespace UserAccess.Application.Users.GetUserById;
internal sealed class GetUserByIdQueryHandler : IQueryRequestHandler<GetUserByIdQuery, ErrorOr<UserResponse>>
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public GetUserByIdQueryHandler(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<ErrorOr<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _dbConnectionFactory.CreateOpenConnection();

        const string sql = 
            """
            SELECT u.UserId,
            u.Login,
            u.Name,
            u.Email,
            u.Role,
            u.Address,
            u.CreatedDateTime 
            FROM Users u
            WHERE u.UserId = @Id
            """;

        UserResponse? userResponse = await connection.QuerySingleOrDefaultAsync(sql, new { request.Id} );
    
        if (userResponse is null)
        {
            return Error.NotFound("User.NotFound", "User was not found");
        }

        return userResponse;
    }
}