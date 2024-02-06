using System.Data;
using UserAccess.Application.Common;
using ErrorOr;
using UserAccess.Domain.Users.Errors;
using UserAccess.Domain.Users;

namespace UserAccess.Application.Users.GetUserById;

internal sealed class GetUserByIdQueryHandler : IQueryRequestHandler<GetUserByIdQuery, ErrorOr<UserResponse>>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(UserId.Create(request.Id));
        var roles = await _userRepository.GetRolesAsync(request.Id);

        if (user is null)
        {
            return UserErrorsCodes.NotFound;
        }

        UserResponse userResponse = new UserResponse(
            user.Id.Value,
            user.Login,
            user.Name,
            user.Email,
            roles.Select(d => d.RoleCode).ToList(),
            user.CreatedDateTime);

        return userResponse;
    }
}