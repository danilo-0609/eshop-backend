using BuildingBlocks.Application.Queries;
using ErrorOr;
using UserAccess.Application.Abstractions;
using UserAccess.Domain.Common;
using UserAccess.Domain.Users;
using UserAccess.Domain.Users.Errors;

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
            return UserErrorsCodes.NotFound;
        }

        if (Password.CreateUnique(request.Password) != user.Password)
        {
            return UserErrorsCodes.IncorrectPassword;
        }

        string token = _jwtProvider.Generate(user);

        return token;       
    }
}