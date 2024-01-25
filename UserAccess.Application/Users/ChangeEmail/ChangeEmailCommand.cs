using ErrorOr;
using MediatR;
using UserAccess.Application.Common;

namespace UserAccess.Application.Users.ChangeEmail;
internal sealed record ChangeEmailCommand(
    Guid Id,
    string Email) : ICommandRequest<ErrorOr<Unit>>;