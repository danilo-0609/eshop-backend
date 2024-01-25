using Shopping.Application.Common;
using ErrorOr;

namespace Shopping.Application.Orders.Pay;

public sealed record PayOrderCommand(
    Guid OrderId) : ICommandRequest<ErrorOr<Guid>>;
