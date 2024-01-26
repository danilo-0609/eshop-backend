using ErrorOr;
using Shopping.Application.Common;

namespace Shopping.Application.Orders.GetById;

public sealed record GetOrderByIdQuery(Guid OrderId) : ICommandRequest<ErrorOr<OrderResponse>>;
