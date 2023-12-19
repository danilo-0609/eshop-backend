using BuildingBlocks.Application.Commands;
using ErrorOr;
using MediatR;

namespace UserAccess.Application.Users.RemoveUser;

public sealed record RemoveUserCommand(Guid UserId) : ICommandRequest<ErrorOr<Unit>>;