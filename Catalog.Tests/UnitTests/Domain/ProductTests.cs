using Catalog.Domain.Products;
using Catalog.Domain.Products.Errors;
using Catalog.Domain.Products.Events;
using Catalog.Domain.Products.ValueObjects;
using ErrorOr;

namespace Catalog.Tests.UnitTests.Domain;


public sealed class ProductTests
{
    private readonly List<Size> _sizes = new() { new Size("XL"), new Size("M"), new Size("L") };
    private readonly List<Color> _colors = new() { new Color("Yellow"), new Color("Black"), new Color("Blue") };
    private readonly List<Tag> _tags = new List<Tag>() { Tag.Create("#ForMan"), Tag.Create("#XL"), Tag.Create("#Casual") };

    [Fact]
    public void Publish_Should_ReturnAnErrorCannotPublishedWithNoStock_WhenPublishFailed()
    {
        //Arrange
        ErrorOr<Product> product = Product.Publish(
            Guid.NewGuid(),
            "TShirt name",
            100.32m,
            "Product description",
            _sizes,
            ProductType.TShirt,
            _tags,
            0,
            DateTime.UtcNow,
            _colors);

        //Act
        bool returnsCannotPublished = product
            .Errors
            .Any(r => r.Code == ProductErrorCodes.CannotPublishWithNoStock.Code);

        //Assert
        Assert.True(returnsCannotPublished);
    }

    [Fact]
    public void Publish_Should_RaiseProductPublishedDomainEvent_WhenIsPublishedSuccessfully()
    {
        //Arrange
        ErrorOr<Product> product = Product.Publish(
            Guid.NewGuid(),
            "TShirt name",
            100.32m,
            "Product description",
            _sizes,
            ProductType.TShirt,
            _tags,
            100,
            DateTime.UtcNow,
            _colors);

        //Act 
        bool raisedProductPublishedDomainEvent = product
            .Value
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
            _sizes,
            ProductType.Jacket,
            _tags,
            91,
            DateTime.UtcNow.AddDays(-10),
            DateTime.UtcNow,
            _colors);

        //Act

        bool raisedProductUpdatedDomainEvent = product
            .DomainEvents
            .Any(t => t.GetType() == typeof(ProductUpdatedDomainEvent));

        //Assert
        Assert.True(raisedProductUpdatedDomainEvent);
    }

    [Fact]
    public void Remove_Should_RaiseProductRemovedDomainEvent()
    {
        //Arrange
        ErrorOr<Product> product = Product.Publish(
            Guid.NewGuid(),
            "TShirt name",
            100.32m,
            "Product description",
            _sizes,
            ProductType.TShirt,
            _tags,
            100,
            DateTime.UtcNow,
            _colors);

        //Act 
        product.Value.Remove();

        bool raisedProductRemovedDomainEvent = product
            .Value
            .DomainEvents
            .Any(t => t.GetType() == typeof(ProductRemovedDomainEvent));

        //Asert
        Assert.True(raisedProductRemovedDomainEvent);
    }

    [Fact]
    public void OutOfStock_Should_ReturnAnError_WhenStockIsNotEmpty()
    {
        //Arrange
        ErrorOr<Product> product = Product.Publish(
            Guid.NewGuid(),
            "Jacket name",
            198.32m,
            "Product description",
            _sizes,
            ProductType.Jacket,
            _tags,
            20,
            DateTime.UtcNow,
            _colors);

        //Act
        var isOutOfStock = product.Value.OutOfStock();

        bool isError = isOutOfStock.IsError;

        //Arrange
        Assert.True(isError);
    }

    [Fact]
    public void OutOfStock_Should_RaiseProductOutOfStockDomainEvent_WhenIsSuccessfullyValidated()
    {
        //Arrange
        ErrorOr<Product> product = Product.Publish(
            Guid.NewGuid(),
            "Jacket name",
            198.32m,
            "Product description",
            _sizes,
            ProductType.Jacket,
            _tags,
            0,
            DateTime.UtcNow,
            _colors);

        //Act
        product.Value.OutOfStock();

        bool raisedProductOutOfStockDomainEvent = product
            .Value
            .DomainEvents
            .Any(r => r.GetType() == typeof(ProductOutOfStockDomainEvent));

        //Assert
        Assert.True(raisedProductOutOfStockDomainEvent);
    }

    [Fact]
    public void Sell_Should_ReturnAnError_WhenTheProductIsOutOfStock()
    {
        //Arrange
        Product product = Product.Update(
            ProductId.CreateUnique(),
            Guid.NewGuid(),
            "Jacket name",
            198.32m,
            "Product description",
            _sizes,
            ProductType.Jacket,
            _tags,
            0,
            DateTime.UtcNow.AddDays(-10),
            DateTime.UtcNow,
            _colors);

        //Act
        var sellingOperation = product.Sell(3, Guid.NewGuid());

        bool operationFailed = sellingOperation
            .Errors.Any(r => r.Code == ProductErrorCodes.ProductOutOfStock.Code);

        //Assert
        Assert.True(operationFailed);
    }

    [Fact]
    public void Sell_Should_RaiseProductOutOfStockDomainEvent_WhenProductIsOutOfStock()
    {
        //Arrange
        ErrorOr<Product> product = Product.Publish(
            Guid.NewGuid(),
            "Jacket name",
            198.32m,
            "Product description",
            _sizes,
            ProductType.Jacket,
            _tags,
            0,
            DateTime.UtcNow,
            _colors);

        //Act
        var sellingOperation = product.Value.Sell(3, Guid.NewGuid());

        bool hasProductOutOfStockDomainEvent = product
            .Value
            .DomainEvents
            .Any(g => g.GetType() == typeof(ProductOutOfStockDomainEvent));
    }

    [Fact]
    public void Sell_Should_ReturnAnError_WhenTheAmountOfProductsRequestedIsGreaterThanActualAmountInStock()
    {
        //Arrange
        ErrorOr<Product> product = Product.Publish(
            Guid.NewGuid(),
            "Jacket name",
            198.32m,
            "Product description",
            _sizes,
            ProductType.Jacket,
            _tags,
            10,
            DateTime.UtcNow,
            _colors);

        //Act
        var sellingOperation = product.Value.Sell(15, Guid.NewGuid());

        bool sellingOperationFailed = sellingOperation
            .Errors
            .Any(r => r.Code == ProductErrorCodes.AmountOfProductsRequestedGreaterThanAmountOfProductsInStock.Code);

        //Assert
        Assert.True(sellingOperationFailed);
    }

    [Fact]
    public void Sell_Should_RaiseProductSellFailedDomainEvent_WhenSellingFailed()
    {
        //Arrange
        ErrorOr<Product> product = Product.Publish(
            Guid.NewGuid(),
            "Jacket name",
            198.32m,
            "Product description",
            _sizes,
            ProductType.Jacket,
            _tags,
            10,
            DateTime.UtcNow,
            _colors);

        //Act
        var sellingOperation = product.Value.Sell(15, Guid.NewGuid());

        bool raisedProductSellFailedDomainEvent = product
            .Value
            .DomainEvents
            .Any(t => t.GetType() == typeof(ProductSellFailedDomainEvent));
    }

    [Fact]
    public void Sell_Should_RaiseProductSoldDomainEvent_WhenIsSuccesfullySold()
    {
        //Arrange
        ErrorOr<Product> product = Product.Publish(
            Guid.NewGuid(),
            "TShirt name",
            100.32m,
            "Product description",
            _sizes,
            ProductType.TShirt,
            _tags,
            100,
            DateTime.UtcNow,
            _colors);

        //Act
        product.Value.Sell(3, Guid.NewGuid());

        bool raisedProductSoldDomainEvent = product
            .Value
            .DomainEvents
            .Any(e => e.GetType() == typeof(ProductSoldDomainEvent));

        //Assert
        Assert.True(raisedProductSoldDomainEvent);
    }
}

