using Catalog.Domain.Products;
using Catalog.Domain.Products.Errors;
using Catalog.Domain.Products.Events;

namespace Catalog.Tests.UnitTests.Domain;


public sealed class ProductTests
{
    [Fact]
    public void Publish_Should_RaiseProductPublishedDomainEvent_WhenIsPublishedSuccessfully()
    {
        //Arrange
        Product product = Product.Publish(
            Guid.NewGuid(),
            "TShirt name",
            100.32m,
            "Product description",
            "XL",
            ProductType.TShirt,
            new List<Tag>
            {
                Tag.Create("#ForMan"),
                Tag.Create("#XL"),
                Tag.Create("#Casual")
            },
            100,
            DateTime.UtcNow,
            "Yellow");

        //Act 
        bool raisedProductPublishedDomainEvent = product
            .DomainEvents
            .Any(t => t.GetType() == typeof(ProductPublishedDomainEvent));

        //Asert
        Assert.True(raisedProductPublishedDomainEvent);
    }

    [Fact]
    public void Update_Should_RaiseProductUpdatedDomainEvent_WhenProductIsSuccessfullyUpdated()
    {
        //Arrange
        Product product = Product.Update(
            ProductId.CreateUnique(),
            Guid.NewGuid(),
            "Jacket name",
            198.32m,
            "Product description",
            "M",
            ProductType.Jacket,
            new List<Tag>
            {
                Tag.Create("#ForWomen"),
                Tag.Create("#M"),
                Tag.Create("#Formal")
            },
            91,
            DateTime.UtcNow.AddDays(-10),
            DateTime.UtcNow,
            "Black");

        //Act

        bool raisedProductUpdatedDomainEvent = product
            .DomainEvents
            .Any(t => t.GetType() == typeof(ProductUpdatedDomainEvent));

        //Assert
        Assert.True(raisedProductUpdatedDomainEvent);
    }

    [Fact]
    public void Remove_Should_ChangeIsActiveToFalse()
    {
        //Arrange
        Product product = Product.Publish(
            Guid.NewGuid(),
            "TShirt name",
            100.32m,
            "Product description",
            "XL",
            ProductType.TShirt,
            new List<Tag>
            {
                Tag.Create("#ForMan"),
                Tag.Create("#XL"),
                Tag.Create("#Casual")
            },
            100,
            DateTime.UtcNow,
            "Yellow");

        //Act
        product.Remove();

        bool isActiveIsFalse = product.IsActive;

        //Assert
        Assert.False(isActiveIsFalse);
    }

    [Fact]
    public void Remove_Should_SetExpiredDateTime()
    {
        //Arrange
        Product product = Product.Publish(
            Guid.NewGuid(),
            "Jacket name",
            198.32m,
            "Product description",
            "M",
            ProductType.Jacket,
            new List<Tag>
            {
                Tag.Create("#ForWomen"),
                Tag.Create("#M"),
                Tag.Create("#Formal")
            },
            91,
            DateTime.UtcNow,
            "Black");

        //Act
        product.Remove();

        bool hasExpiredDateTime = product.ExpiredDateTime.HasValue;

        //Assert
        Assert.True(hasExpiredDateTime);
    }

    [Fact]
    public void Remove_Should_RaiseProductRemovedDomainEvent()
    {
        //Arrange
        Product product = Product.Publish(
            Guid.NewGuid(),
            "TShirt name",
            100.32m,
            "Product description",
            "XL",
            ProductType.TShirt,
            new List<Tag>
            {
                Tag.Create("#ForMan"),
                Tag.Create("#XL"),
                Tag.Create("#Casual")
            },
            100,
            DateTime.UtcNow,
            "Yellow");

        //Act 
        product.Remove();

        bool raisedProductRemovedDomainEvent = product
            .DomainEvents
            .Any(t => t.GetType() == typeof(ProductRemovedDomainEvent));

        //Asert
        Assert.True(raisedProductRemovedDomainEvent);
    }

    [Fact]
    public void OutOfStock_Should_ReturnAnError_WhenStockIsNotEmpty()
    {
        //Arrange
        Product product = Product.Publish(
            Guid.NewGuid(),
            "Jacket name",
            198.32m,
            "Product description",
            "M",
            ProductType.Jacket,
            new List<Tag>
            {
                Tag.Create("#ForWomen"),
                Tag.Create("#M"),
                Tag.Create("#Formal")
            },
            20,
            DateTime.UtcNow,
            "Black");

        //Act
        var isOutOfStock = product.OutOfStock();

        bool isError = isOutOfStock.IsError;

        //Arrange
        Assert.True(isError);

    }

    [Fact]
    public void OutOfStock_Should_RaiseProductOutOfStockDomainEvent_WhenIsSuccessfullyValidated()
    {
        //Arrange
        Product product = Product.Publish(
            Guid.NewGuid(),
            "Jacket name",
            198.32m,
            "Product description",
            "M",
            ProductType.Jacket,
            new List<Tag>
            {
                Tag.Create("#ForWomen"),
                Tag.Create("#M"),
                Tag.Create("#Formal")
            },
            0,
            DateTime.UtcNow,
            "Black");

        //Act
        product.OutOfStock();

        bool raisedProductOutOfStockDomainEvent = product
            .DomainEvents
            .Any(r => r.GetType() == typeof(ProductOutOfStockDomainEvent));

        //Assert
        Assert.True(raisedProductOutOfStockDomainEvent);
    }

    [Fact]
    public void Sell_Should_ReturnAnError_WhenTheProductIsOutOfStock()
    {
        //Arrange
        Product product = Product.Publish(
            Guid.NewGuid(),
            "Jacket name",
            198.32m,
            "Product description",
            "M",
            ProductType.Jacket,
            new List<Tag>
            {
                Tag.Create("#ForWomen"),
                Tag.Create("#M"),
                Tag.Create("#Formal")
            },
            0,
            DateTime.UtcNow,
            "Black");

        //Act
        var sellingOperation = product.Sell(3);

        bool operationFailed = sellingOperation
            .Errors.Any(r => r.Code == ProductErrors.ProductOutOfStock.Code);

        //Assert
        Assert.True(operationFailed);
    }

    [Fact]
    public void Sell_Should_RaiseProductOutOfStockDomainEvent_WhenProductIsOutOfStock()
    {
        //Arrange
        Product product = Product.Publish(
            Guid.NewGuid(),
            "Jacket name",
            198.32m,
            "Product description",
            "M",
            ProductType.Jacket,
            new List<Tag>
            {
                Tag.Create("#ForWomen"),
                Tag.Create("#M"),
                Tag.Create("#Formal")
            },
            0,
            DateTime.UtcNow,
            "Black");

        //Act
        var sellingOperation = product.Sell(3);

        bool hasProductOutOfStockDomainEvent = product
            .DomainEvents
            .Any(g => g.GetType() == typeof(ProductOutOfStockDomainEvent));
    }

    [Fact]
    public void Sell_Should_ReturnAnError_WhenTheAmountOfProductsRequestedIsGreaterThanActualAmountInStock()
    {
        //Arrange
        Product product = Product.Publish(
            Guid.NewGuid(),
            "Jacket name",
            198.32m,
            "Product description",
            "M",
            ProductType.Jacket,
            new List<Tag>
            {
                Tag.Create("#ForWomen"),
                Tag.Create("#M"),
                Tag.Create("#Formal")
            },
            10,
            DateTime.UtcNow,
            "Black");

        //Act
        var sellingOperation = product.Sell(15);

        bool sellingOperationFailed = sellingOperation
            .Errors
            .Any(r => r.Code == ProductErrors.AmountOfProductsRequestedGreaterThanAmountOfProductsInStock.Code);

        //Assert
        Assert.True(sellingOperationFailed);
    }

    [Fact]
    public void Sell_Should_RaiseProductSellFailedDomainEvent_WhenSellingFailed()
    {
        //Arrange
        Product product = Product.Publish(
            Guid.NewGuid(),
            "Jacket name",
            198.32m,
            "Product description",
            "M",
            ProductType.Jacket,
            new List<Tag>
            {
                Tag.Create("#ForWomen"),
                Tag.Create("#M"),
                Tag.Create("#Formal")
            },
            10,
            DateTime.UtcNow,
            "Black");

        //Act
        var sellingOperation = product.Sell(15);

        bool raisedProductSellFailedDomainEvent = product
            .DomainEvents
            .Any(t => t.GetType() == typeof(ProductSellFailedDomainEvent));
    }

    [Fact]
    public void Sell_Should_RaiseProductSoldDomainEvent_WhenIsSuccesfullySold()
    {
        //Arrange
        Product product = Product.Publish(
            Guid.NewGuid(),
            "TShirt name",
            100.32m,
            "Product description",
            "XL",
            ProductType.TShirt,
            new List<Tag>
            {
                Tag.Create("#ForMan"),
                Tag.Create("#XL"),
                Tag.Create("#Casual")
            },
            100,
            DateTime.UtcNow,
            "Yellow");

        //Act
        product.Sell(3);

        bool raisedProductSoldDomainEvent = product
            .DomainEvents
            .Any(e => e.GetType() == typeof(ProductSoldDomainEvent));

        //Assert
        Assert.True(raisedProductSoldDomainEvent);
    }
}

