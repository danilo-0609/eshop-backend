using BuildingBlocks.Domain;
using Catalog.Domain.Products.Events;
using Catalog.Domain.Products.Rules;
using ErrorOr;
using MediatR;

namespace Catalog.Domain.Products;

public sealed class Product : AggregateRoot<ProductId, Guid>
{
    public new ProductId Id { get; private set; }

    public Guid SellerId { get; private set; }

    public string Name { get; private set; }

    public decimal Price { get; private set; }

    public string Description { get; private set; }

    public string Size { get; private set; }

    public string Color { get; private set; } = string.Empty;

    public ProductType ProductType { get; private set; }

    public List<Tag> Tags { get; private set; }

    public int InStock { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime CreatedDateTime { get; private set; }

    public DateTime? UpdatedDateTime { get; private set; }

    public DateTime? ExpiredDateTime { get; private set; }

    public static Product Publish(Guid sellerId,
        string name,
        decimal price,
        string description,
        string size, 
        ProductType productType,
        List<Tag> tags,
        int inStock,
        DateTime ocurredOn,
        string color = "")
    {
        Product product = new Product(ProductId.CreateUnique(),
            sellerId,
            name,
            price,
            description,
            size,
            color,
            productType,
            tags,
            inStock,
            isActive: true,
            ocurredOn);

        ProductPublishedDomainEvent productPublishedDomainEvent = new(Guid.NewGuid(), 
            product.Id,
            product.SellerId,
            product.Name,
            product.Description,
            product.Price,
            product.InStock,
            product.Size,
            product.Tags,
            product.ProductType,
            product.CreatedDateTime,
            product.Color);

        product.Raise(productPublishedDomainEvent);

        return product;
    }


    public static Product Update(ProductId id,
        Guid sellerId,
        string name,
        decimal price,
        string description,
        string size,
        ProductType productType,
        List<Tag> tags,
        int inStock,
        DateTime createdOn,
        DateTime updatedOn,
        string color = "")
        {
            var product = new Product(id,
                sellerId,
                name,
                price,
                description,
                size,
                color,
                productType,
                tags,
                inStock,
                isActive: true,
                createdOn,
                updatedOn);

            ProductUpdatedDomainEvent productUpdatedDomainEvent = new(Guid.NewGuid(),
                product.Id,
                product.UpdatedDateTime!.Value);

            product.Raise(productUpdatedDomainEvent);

            return product;
        }

    public void Remove()
    {
        ExpiredDateTime = DateTime.UtcNow;
        IsActive = false;

        ProductRemovedDomainEvent productRemovedDomainEvent = new(Guid.NewGuid(), 
            Id,
            ExpiredDateTime.Value);

        Raise(productRemovedDomainEvent);
    }
    
    public ErrorOr<Unit> OutOfStock()
    {
        if (InStock != 0)
        {
            return Error.Validation("Product.HasStock", "Product is not out of stock");
        }

        Raise(new ProductOutOfStockDomainEvent(Guid.NewGuid(), Id, DateTime.UtcNow));
        
        return Unit.Value;
    }

    public ErrorOr<Unit> Sell(int amountOfProducts, Guid orderId)
    {
        var isOutOfStockRule = CheckRule(new ProductCannotBeSoldWhenProductIsOutOfStockRule(InStock));

        if (isOutOfStockRule.IsError)
        {
            Raise(new ProductOutOfStockDomainEvent(
                Guid.NewGuid(),
                Id,
                DateTime.UtcNow));

            return isOutOfStockRule.FirstError;
        }

        var isAmountOfProductsRequestedGreaterThanActualInStock = CheckRule(new ProductCannotBeSoldWhenAmountOfProductsInBuyingRequestIsGreaterThanActualInStockRule(amountOfProducts, InStock));

        if (isAmountOfProductsRequestedGreaterThanActualInStock.IsError)
        {
            Raise(new ProductSellFailedDomainEvent(
                Guid.NewGuid(),
                Id,
                orderId,
                ProductCannotBeSoldWhenAmountOfProductsInBuyingRequestIsGreaterThanActualInStockRule.Message,
                DateTime.UtcNow));

            return isAmountOfProductsRequestedGreaterThanActualInStock.FirstError;
        }

        InStock = InStock - amountOfProducts;

        Raise(new ProductSoldDomainEvent(Guid.NewGuid(), 
            Id, 
            amountOfProducts,
            Price,
            SellerId,
            orderId,
            DateTime.UtcNow));

        return Unit.Value;
    }

    public Product(ProductId id,
        Guid sellerId,
        string name,
        decimal price,
        string description,
        string size,
        string color,
        ProductType productType,
        List<Tag> tags,
        int inStock,
        bool isActive,
        DateTime createdDateTime,
        DateTime? updatedDateTime = null,
        DateTime? expiredDateTime = null)
        : base(id)
    {
        Id = id;
        Name = name;
        Price = price;
        Description = description;
        Size =  size;
        Color = color;
        ProductType = productType;
        Tags = tags;
        InStock = inStock;
        IsActive = isActive;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
        ExpiredDateTime = expiredDateTime;
    }

    private Product(){}
}