using BuildingBlocks.Application.Commands;
using ErrorOr;
using MediatR;

namespace Shopping.Application.Orders.Expire;

public sealed record ExpireOrderCommand(
    Guid OrderId) : ICommandRequest<ErrorOr<Unit>>;
