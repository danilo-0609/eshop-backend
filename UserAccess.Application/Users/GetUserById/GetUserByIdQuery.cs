using UserAccess.Application.Common;
using ErrorOr;

namespace UserAccess.Application.Users.GetUserById;

public sealed record GetUserByIdQuery(Guid Id) : IQueryRequest<ErrorOr<UserResponse>>;