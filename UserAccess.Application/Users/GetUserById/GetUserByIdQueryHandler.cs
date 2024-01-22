using System.Data;
using BuildingBlocks.Application.Queries;
using Dapper;
using ErrorOr;
using UserAccess.Application.Abstractions;

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
            r.RoleCode as Role,
            u.Address,
            u.CreatedDateTime 
            FROM users.Users u
            INNER JOIN users.UsersRoles ur ON u.UserId = ur.UserId
            INNER JOIN Roles r ON ur.RoleId = r.RoleId
            WHERE u.UserId = @Id
            """;

        UserResponse? userResponse = await connection.QuerySingleOrDefaultAsync<UserResponse>(sql, new { request.Id} );
    
        if (userResponse is null)
        {
            return Error.NotFound("User.NotFound", "User was not found");
        }

        return userResponse;
    }
}