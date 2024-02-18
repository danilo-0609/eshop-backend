using UserAccess.Application.Common;
using ErrorOr;
using UserAccess.Application.UserRegistration.GetUserRegistrationByIda;
using UserAccess.Domain.UserRegistrations;
using UserAccess.Domain.UserRegistrations.Errors;
using UserAccess.Application.Abstractions;

namespace UserAccess.Application.UserRegistration.GetUserRegistrationById;

internal sealed class GetUserRegistrationByIdQueryHandler : IQueryRequestHandler<GetUserRegistrationByIdQuery, ErrorOr<UserRegistrationResponse>>
{
    private readonly IUserRegistrationRepository _userRegistrationRepository;
    private readonly IAuthorizationService _authorizationService;

    public GetUserRegistrationByIdQueryHandler(IUserRegistrationRepository userRegistrationRepository, IAuthorizationService authorizationService)
    {
        _userRegistrationRepository = userRegistrationRepository;
        _authorizationService = authorizationService;
    }

    public async Task<ErrorOr<UserRegistrationResponse>> Handle(GetUserRegistrationByIdQuery request, CancellationToken cancellationToken)
    {
        var userRegistration = await _userRegistrationRepository
                                     .GetByIdAsync(UserRegistrationId.Create(request.UserRegistrationId));
        
        if (userRegistration is null)
        {
            return UserRegistrationErrors.NotFound;
        }

        if (_authorizationService.IsAdmin() is false)
        {
            return Error.Unauthorized("UserRegistration.Unauthorized", "Cannot access to this content if you are not an admin user");
        }

        return new UserRegistrationResponse(
            userRegistration.Id.Value,
            userRegistration.Login,
            userRegistration.Email,
            userRegistration.Name,
            userRegistration.RegisteredDate,
            userRegistration.Status.Value,
            userRegistration.ConfirmedDate);
    }
}
