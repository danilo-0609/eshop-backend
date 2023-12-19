using System.Data;
using BuildingBlocks.Application.Queries;
using Dapper;
using ErrorOr;
using UserAccess.Application.Abstractions;
using UserAccess.Application.Common;
using UserAccess.Domain.Common;
using UserAccess.Domain.Users;

namespace UserAccess.Application.Users.Login;
internal sealed class LoginUserQueryHandler : IQueryRequestHandler<LoginUserQuery, ErrorOr<string>>
{   
    private readonly IDbConnectionFactory _dbConnectionFactory;
    private readonly IJwtProvider _jwtProvider;

    public LoginUserQueryHandler(IDbConnectionFactory dbConnectionFactory, IJwtProvider jwtProvider)
    {
        _dbConnectionFactory = dbConnectionFactory;
        _jwtProvider = jwtProvider;
    }

    public async Task<ErrorOr<string>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _dbConnectionFactory.CreateOpenConnection();

        const string sql = 
            """
            SELECT u.UserId,
            u.Login,
            u.Password,
            u.Name,
            u.Email,
            u.IsActive,
            u.FirstName,
            u.LastName,
            u.Role,
            u.Address,
            u.CreatedDateTime 
            FROM Users u
            WHERE u.Email = @Email
            """;

        User? user = await connection.QuerySingleOrDefaultAsync(sql, new { request.Email });

        if (user is null)
        {
            return Error.NotFound("User.NotFound", "User was not found");
        }

        if (Password.Create(request.Password) != user.Password)
        {
            return Error.Validation("Password.Incorrect", "The password is not correct");
        }

        string token = _jwtProvider.Generate(user);

        return token;       
    }
}