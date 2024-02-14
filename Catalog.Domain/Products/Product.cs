using BuildingBlocks.Domain;
using Catalog.Domain.Products.Events;
using Catalog.Domain.Products.Rules;
using Catalog.Domain.Products.ValueObjects;
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

    public List<Size> Sizes { get; private set; }

    public List<Color> Colors { get; private set; }

    public ProductType ProductType { get; private set; }

    public List<Tag> Tags { get; private set; }

    public int InStock { get; private set; }

    public StockStatus StockStatus { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime CreatedDateTime { get; private set; }

    public DateTime? UpdatedDateTime { get; private set; }

    public DateTime? ExpiredDateTime { get; private set; }


    public static ErrorOr<Product> Publish(Guid sellerId,
        string name,
        decimal price,
        string description,
        List<Size> sizes, 
        ProductType productType,
        List<Tag> tags,
        int inStock,
        DateTime ocurredOn,
        List<Color> colors)
    {
        Product product = new Product(ProductId.CreateUnique(),
            sellerId,
            name,
            price,
            description,
            sizes,
            colors,
            productType,
            tags,
            inStock,
            isActive: true,
            ocurredOn);

        var cannotBePublishedWithNoStock = product.CheckRule(new ProductCannotBePublishedWithNoStockRule(product.StockStatus));

        if (cannotBePublishedWithNoStock.IsError)
        {
            return cannotBePublishedWithNoStock.FirstError;
        }

        ProductPublishedDomainEvent productPublishedDomainEvent = new(Guid.NewGuid(), 
            product.Id,
            product.SellerId,
            product.Name,
            product.Description,
            product.Price,
            product.InStock,
            product.CreatedDateTime);

        product.Raise(productPublishedDomainEvent);

        return product;
    }


    public static Product Update(ProductId id,
        Guid sellerId,
        string name,
        decimal price,
        string description,
        List<Size> sizes,
        ProductType productType,
        List<Tag> tags,
        int inStock,
        DateTime createdOn,
        DateTime updatedOn,
        List<Color> colors)
        {
            var product = new Product(id,
                sellerId,
                name,
                price,
                description,
                sizes,
                colors,
                productType,
                tags,
                inStock,
                isActive: true,
                createdOn,
                updatedOn,
                null);

            ProductUpdatedDomainEvent productUpdatedDomainEvent = new(Guid.NewGuid(),
                product.Id,
                product.Name,
                product.SellerId,
                product.Price,
                product.InStock,
                product.UpdatedDateTime!.Value);

            product.Raise(productUpdatedDomainEvent);

            return product;
        }

    public void Remove()
    {
        ProductRemovedDomainEvent productRemovedDomainEvent = new(Guid.NewGuid(), 
            Id,
            DateTime.UtcNow);

        Raise(productRemovedDomainEvent);
    }
    
    public ErrorOr<Unit> OutOfStock()
    {
        if (StockStatus == StockStatus.OutOfStock)
        {
            return Error.Validation("Product.HasStock", "Product is not out of stock");
        }

        Raise(new ProductOutOfStockDomainEvent(Guid.NewGuid(), Id, DateTime.UtcNow));
        
        return Unit.Value;
    }

    public ErrorOr<Unit> Sell(int amountOfProducts, Guid orderId)
    {
        var isOutOfStockRule = CheckRule(new ProductCannotBeSoldWhenProductIsOutOfStockRule(StockStatus));

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

    private StockStatus CheckStatus()
    {
        if (InStock == 0)
        {
            return StockStatus.OutOfStock;
        }

        return StockStatus.WithStock;
    }

    public Product(ProductId id,
        Guid sellerId,
        string name,
        decimal price,
        string description,
        List<Size> sizes,
        List<Color> colors,
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
        SellerId = sellerId;
        Name = name;
        Price = price;
        Description = description;
        Sizes =  sizes;
        Colors = colors;
        ProductType = productType;
        Tags = tags;
        InStock = inStock;
        StockStatus = CheckStatus();
        IsActive = isActive;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
        ExpiredDateTime = expiredDateTime;
    }

    private Product(){}
}