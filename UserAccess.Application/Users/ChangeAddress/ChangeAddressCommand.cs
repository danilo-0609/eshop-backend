using ErrorOr;
using MediatR;
using UserAccess.Application.Common;

namespace UserAccess.Application.Users.ChangeAddress;

public sealed record ChangeEmailCommand(
    Guid Id,
    string Address) : ICommandRequest<ErrorOr<Unit>>;