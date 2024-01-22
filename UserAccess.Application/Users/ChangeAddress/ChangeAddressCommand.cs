using BuildingBlocks.Application.Commands;
using ErrorOr;
using MediatR;

namespace UserAccess.Application.Users.ChangeAddress;

public sealed record ChangeEmailCommand(
    Guid Id,
    string Address) : ICommandRequest<ErrorOr<Unit>>;