using BuildingBlocks.Application.Commands;
using ErrorOr;
using MediatR;

namespace UserAccess.Application.Users.ChangeEmail;
internal sealed record ChangeEmailCommand(
    Guid Id,
    string Email) : ICommandRequest<ErrorOr<Unit>>;