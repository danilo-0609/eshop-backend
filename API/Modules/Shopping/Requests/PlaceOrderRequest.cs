namespace API.Modules.Shopping.Requests;

public sealed record PlaceOrderRequest(
    Guid ItemId,
    int AmountRequested);
