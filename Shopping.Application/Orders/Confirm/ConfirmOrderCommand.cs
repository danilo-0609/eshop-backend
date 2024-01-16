using BuildingBlocks.Application.Commands;
using ErrorOr;

namespace Shopping.Application.Orders.Confirm;

public sealed record ConfirmOrderCommand(
    Guid OrderId) : ICommandRequest<ErrorOr<Guid>>;
