using System.Data;
using BuildingBlocks.Application.Queries;
using Dapper;
using ErrorOr;
using UserAccess.Application.Abstractions;
using UserAccess.Domain.Common;
using UserAccess.Domain.Users;

namespace UserAccess.Application.Users.Login;
internal sealed class LoginUserQueryHandler : IQueryRequestHandler<LoginUserQuery, ErrorOr<string>>
{   
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;

    public LoginUserQueryHandler(IJwtProvider jwtProvider, IUserRepository userRepository)
    {
        _jwtProvider = jwtProvider;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<string>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

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