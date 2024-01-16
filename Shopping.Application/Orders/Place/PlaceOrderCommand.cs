using BuildingBlocks.Application.Commands;
using ErrorOr;

namespace Shopping.Application.Orders.Place;

public sealed record PlaceOrderCommand(
    Guid ItemId,
    int AmountRequested) : ICommandRequest<ErrorOr<Guid>>;
