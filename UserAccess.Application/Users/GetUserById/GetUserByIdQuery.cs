using BuildingBlocks.Application.Queries;
using ErrorOr;

namespace UserAccess.Application.Users.GetUserById;
public sealed record GetUserByIdQuery(Guid Id) : IQueryRequest<ErrorOr<UserResponse>>;