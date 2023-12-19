using BuildingBlocks.Application.Queries;
using ErrorOr;

namespace UserAccess.Application.UserRegistration.GetUserRegistrationByIda;

public sealed record GetUserRegistrationByIdQuery(Guid UserRegistrationId) 
        : IQueryRequest<ErrorOr<UserRegistrationResponse>>;
