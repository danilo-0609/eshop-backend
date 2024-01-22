using BuildingBlocks.Application.Commands;
using ErrorOr;
using MediatR;

namespace UserAccess.Application.Users.ChangePassword;
public sealed record ChangePasswordCommand(
    Guid Id, 
    string OldPassword,
    string NewPassword) : ICommandRequest<ErrorOr<Unit>>;