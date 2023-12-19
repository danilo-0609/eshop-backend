using BuildingBlocks.Domain;

namespace Shopping.Domain.Buying;
public sealed class Buy : AggregateRoot<BuyId, Guid>
{
    public new BuyId Id { get; private set; }

    private Buy(BuyId id)
    {
        Id = id;
    }

    private Buy(){}
}