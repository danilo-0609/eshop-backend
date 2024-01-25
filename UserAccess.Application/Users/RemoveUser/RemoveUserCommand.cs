using UserAccess.Application.Common;
using ErrorOr;
using MediatR;

namespace UserAccess.Application.Users.RemoveUser;

public sealed record RemoveUserCommand(Guid UserId) : ICommandRequest<ErrorOr<Unit>>;