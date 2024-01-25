using UserAccess.Application.Common;
using ErrorOr;

namespace UserAccess.Application.UserRegistration.GetUserRegistrationByIda;

public sealed record GetUserRegistrationByIdQuery(Guid UserRegistrationId) 
        : IQueryRequest<ErrorOr<UserRegistrationResponse>>;
