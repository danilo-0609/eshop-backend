using BuildingBlocks.Domain;

namespace Shopping.Domain.Basket;
public sealed class Basket : AggregateRoot<BasketId, Guid>
{   
    public new BasketId Id { get; private set; }

    private Basket(BasketId id)
    {
        Id = id;
    }
}