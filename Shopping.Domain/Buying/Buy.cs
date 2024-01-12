using BuildingBlocks.Domain;
using Shopping.Domain.Buying.Events;
using Shopping.Domain.Items;

namespace Shopping.Domain.Buying;

public sealed class Buy : AggregateRoot<BuyId, Guid>
{
    public new BuyId Id { get; private set; }

    public Guid BuyerId { get; private set; }

    public ItemId ItemId { get; private set; }

    public int AmountOfProducts { get; private set; }

    public decimal UnitPrice { get; private set; }

    public decimal TotalAmount {  get; private set; }

    public DateTime OcurredOn { get; private set; }

    private Buy(
        BuyId id,
        Guid buyerId,
        ItemId itemId,
        int amountOfProducts,
        decimal unitPrice,
        DateTime ocurredOn)
    {
        Id = id;
        BuyerId = buyerId;
        ItemId = itemId;
        AmountOfProducts = amountOfProducts;
        UnitPrice = unitPrice;

        TotalAmount = UnitPrice * AmountOfProducts;
        OcurredOn = ocurredOn;
    }

    public static Buy Generate(
        Guid buyerId,
        ItemId itemId,
        int amountOfProducts,
        decimal unitPrice,
        DateTime ocurredOn)
    {
        var buy = new Buy(
            BuyId.CreateUnique(),
            buyerId,
            itemId,
            amountOfProducts,
            unitPrice,
            ocurredOn);

        buy.Raise(new BuyGeneratedDomainEvent(
            Guid.NewGuid(),
            buy.Id,
            buy.BuyerId,
            buy.ItemId,
            buy.UnitPrice,
            buy.TotalAmount,
            buy.AmountOfProducts,
            DateTime.UtcNow));

        return buy;
    }

    private Buy(){}
}