using BuildingBlocks.Application.Commands;
using ErrorOr;
using MediatR;

namespace UserAccess.Application.UserRegistration.ConfirmUserRegistration;
public sealed record ConfirmUserRegistrationCommand(Guid UserRegistrationId) : ICommandRequest<ErrorOr<Unit>>;