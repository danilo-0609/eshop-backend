using BuildingBlocks.Application.Commands;
using ErrorOr;

namespace UserAccess.Application.UserRegistration.RegisterNewUser;

public sealed record RegisterNewUserCommand(
    string Login,
    string Password,
    string Email,
    string FirstName,
    string LastName,
    string Address) : ICommandRequest<ErrorOr<Guid>>;
