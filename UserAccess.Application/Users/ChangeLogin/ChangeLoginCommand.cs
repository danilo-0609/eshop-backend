using ErrorOr;
using MediatR;
using UserAccess.Application.Common;

namespace UserAccess.Application.Users.ChangeLogin;

public sealed record ChangeLoginCommand(
    Guid Id,
    string Login) : ICommandRequest<ErrorOr<Unit>>;