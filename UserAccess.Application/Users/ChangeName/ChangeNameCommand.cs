using UserAccess.Application.Common;
using ErrorOr;
using MediatR;

namespace UserAccess.Application.Users.ChangeName;

public sealed record ChangeNameCommand(
    Guid Id,
    string FirstName,
    string LastName) : ICommandRequest<ErrorOr<Unit>>;