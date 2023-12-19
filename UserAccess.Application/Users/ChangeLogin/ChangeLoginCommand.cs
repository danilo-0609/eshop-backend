using BuildingBlocks.Application.Commands;
using ErrorOr;
using MediatR;

namespace UserAccess.Application.Users.ChangeLogin;
public sealed record ChangeLoginCommand(
    Guid Id,
    string Login) : ICommandRequest<ErrorOr<Unit>>;