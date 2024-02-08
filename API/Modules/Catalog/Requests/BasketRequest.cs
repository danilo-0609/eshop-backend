namespace API.Modules.Catalog.Requests;

public sealed record BasketRequest(Guid ItemId, int Amount);
