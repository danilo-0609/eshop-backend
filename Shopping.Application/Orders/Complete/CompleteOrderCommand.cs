using Shopping.Application.Common;
using ErrorOr;
using MediatR;

namespace Shopping.Application.Orders.Complete;

internal sealed record CompleteOrderCommand(Guid OrderId) : ICommandRequest<ErrorOr<Unit>>;
