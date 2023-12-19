using BuildingBlocks.Domain;
using Catalog.Domain.Products;
using Catalog.Domain.Sales.Events;

namespace Catalog.Domain.Sales;

public sealed class Sale : AggregateRoot<SaleId, Guid>
{
    public new SaleId Id { get; private set; }
    
    public ProductId ProductId { get; private set; }

    public int AmountOfProducts { get; private set; } 

    public decimal UnitPrice { get; private set; }

    public Guid UserId { get; private set; }

    public DateTime CreatedDateTime { get; private set; }


    public static Sale Generate(
        ProductId productId,
        int amountOfProducts,
        decimal unitPrice,
        Guid userId,
        DateTime ocurredOn)
    {
        Sale sale = new Sale(
            SaleId.CreateUnique(),
            productId,
            amountOfProducts,
            unitPrice,
            userId,
            ocurredOn);

        sale.Raise(new SaleGeneratedDomainEvent(Guid.NewGuid(),
            sale.ProductId,
            amountOfProducts,
            sale.UserId,
            sale.CreatedDateTime));

        return sale;
    }

    private Sale(
        SaleId id, 
        ProductId productId, 
        int amountOfProducts,
        decimal total, 
        Guid userId, 
        DateTime createdDateTime)
    {
        Id = id;
        ProductId = productId;
        AmountOfProducts = amountOfProducts;
        UnitPrice = total;
        UserId = userId;
        CreatedDateTime = createdDateTime;
    }        

    public Sale() { }
}
