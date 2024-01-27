using ErrorOr;
using Shopping.Domain.Items;
using Shopping.Domain.Orders;
using Shopping.Domain.Orders.Events;

namespace Shopping.UnitTests.UnitTests.Domain;

public sealed class OrderTests
{
    [Fact]
    public void Place_Should_ReturnErrorItemIsOutOfStock_WhenItemStockIsOutOfStock()
    {
        //Arrange
        Item item = Item.Create(
            Guid.NewGuid(),
            "Item name",
            Guid.NewGuid(),
            100.32m,
            0,
            DateTime.UtcNow);

        ErrorOr<Order> order = Order.Place(
            item.Id,
            Guid.NewGuid(),
            DateTime.UtcNow,
            0,
            item.InStock,
            item.Price * 10,
            item.StockStatus);

        //Act
        bool responseIsErrorOutOfStock = order.Errors.Any(d => d.Code == "Order.ItemIsOutOfStock");

        //Assert
        Assert.True(responseIsErrorOutOfStock);
    }

    [Fact]
    public void Place_Should_ReturnErrorAmountRequestedGreaterThanActualStock_WhenAmountRequestedIsGreaterThanActualStock()
    {
        //Arrange

        int actualAmount = 10;
        int amountRequested = 15;


        Item item = Item.Create(
            Guid.NewGuid(),
            "Item name",
            Guid.NewGuid(),
            100.32m,
            actualAmount,
            DateTime.UtcNow);

        ErrorOr<Order> order = Order.Place(
            item.Id,
            Guid.NewGuid(),
            DateTime.UtcNow,
            amountRequested,
            item.InStock,
            item.Price * amountRequested,
            item.StockStatus);

        //Act
        bool responseIsErrorAmountRequestedGreaterThanActualStock = order
            .Errors
            .Any(d => d.Code == "Order.AmountRequestedGreaterThanActualStock");

        //Assert
        Assert.True(responseIsErrorAmountRequestedGreaterThanActualStock);
    }

    [Fact]
    public void Place_Should_RaiseOrderPlacedDomainEvent_WhenOrderIsPlacedSuccessfully()
    {
        //Arrange
        Item item = Item.Create(
            Guid.NewGuid(),
            "Item name",
            Guid.NewGuid(),
            100.32m,
            100,
            DateTime.UtcNow);

        ErrorOr<Order> order = Order.Place(
            item.Id,
            Guid.NewGuid(),
            DateTime.UtcNow,
            2,
            item.InStock,
            item.Price * 2,
            item.StockStatus);

        //Act
        bool raisedOrderPlacedDomainEvent = order
            .Value
            .DomainEvents
            .Any(t => t.GetType() == typeof(OrderPlacedDomainEvent));

        //Assert
        Assert.True(raisedOrderPlacedDomainEvent);
    }

    [Fact]
    public void Confirm_Should_ReturnAnErrorOrderStatusAlreadyConfirmed_WhenOrderStatusIsAlreadyConfirmed()
    {
        //Arrange
        Item item = Item.Create(
            Guid.NewGuid(),
            "Item name",
            Guid.NewGuid(),
            100.32m,
            100,
            DateTime.UtcNow);

        ErrorOr<Order> order = Order.Place(
            item.Id,
            Guid.NewGuid(),
            DateTime.UtcNow,
            2,
            item.InStock,
            item.Price * 2,
            item.StockStatus);

        order.Value.Confirm(DateTime.UtcNow);

        var confirm = order.Value.Confirm(DateTime.UtcNow);

        //Act
        bool isReturningAnErrorOrderStatusAlreadyConfirmed = confirm
            .Errors
            .Any(f => f.Code == "Order.StatusAlreadyConfirmed");

        //Assert 
        Assert.True(isReturningAnErrorOrderStatusAlreadyConfirmed);
    }

    [Fact]
    public void Confirm_Should_ReturnAnErrorOrderStatusIsNotPlaced_WhenOrderStatusIsNotPlaced()
    {
        //Arrange
        Item item = Item.Create(
            Guid.NewGuid(),
            "Item name",
            Guid.NewGuid(),
            100.32m,
            100,
            DateTime.UtcNow);

        ErrorOr<Order> order = Order.Place(
            item.Id,
            Guid.NewGuid(),
            DateTime.UtcNow,
            2,
            item.InStock,
            item.Price * 2,
            item.StockStatus);

        order.Value.Confirm(DateTime.UtcNow);

        order.Value.Pay(DateTime.UtcNow, item.InStock, item.StockStatus);

        var confirm = order.Value.Confirm(DateTime.UtcNow);

        //Act
        bool isReturningAnErrorOrderStatusAlreadyConfirmed = confirm
            .Errors
            .Any(f => f.Code == "Order.StatusIsNotPlaced");

        //Assert 
        Assert.True(isReturningAnErrorOrderStatusAlreadyConfirmed);
    }

    [Fact]
    public void Confirm_Should_RaiseOrderConfirmedDomainEvent_WhenOrderConfirmationIsSuccessfully()
    {
        //Arrange
        Item item = Item.Create(
            Guid.NewGuid(),
            "Item name",
            Guid.NewGuid(),
            100.32m,
            100,
            DateTime.UtcNow);

        ErrorOr<Order> order = Order.Place(
            item.Id,
            Guid.NewGuid(),
            DateTime.UtcNow,
            2,
            item.InStock,
            item.Price * 2,
            item.StockStatus);

        //Act
        var confirm = order.Value.Confirm(DateTime.UtcNow);

        bool raisedOrderConfirmedDomainEvent = order
            .Value
            .DomainEvents
            .Any(d => d.GetType() == typeof(OrderConfirmedDomainEvent));
    }

    [Fact]
    public void Expire_Should_ReturnAnErrorOrderStatusAlreadyExpired_WhenOrderStatusIsAlreadyExpired()
    {
        //Arrange
        Item item = Item.Create(
            Guid.NewGuid(),
            "Item name",
            Guid.NewGuid(),
            100.32m,
            100,
            DateTime.UtcNow);

        ErrorOr<Order> order = Order.Place(
            item.Id,
            Guid.NewGuid(),
            DateTime.UtcNow,
            2,
            item.InStock,
            item.Price * 2,
            item.StockStatus);

        //Act
        order.Value.Expire(DateTime.UtcNow);

        var expire = order.Value.Expire(DateTime.UtcNow);

        bool isReturningErrorOrderStatusAlreadyExpired = expire
            .Errors
            .Any(f => f.Code == "Order.StatusAlreadyExpired");

        //Assert
        Assert.True(isReturningErrorOrderStatusAlreadyExpired);
    }

    [Fact]
    public void Expire_Should_ReturnAnErrorOrderStatusIsCompleted_WhenOrderStatusIsAlreadyCompleted()
    {
        Item item = Item.Create(
            Guid.NewGuid(),
            "Item name",
            Guid.NewGuid(),
            100.32m,
            100,
            DateTime.UtcNow);

        ErrorOr<Order> order = Order.Place(
            item.Id,
            Guid.NewGuid(),
            DateTime.UtcNow,
            2,
            item.InStock,
            item.Price * 2,
            item.StockStatus);

        order.Value.Expire(DateTime.UtcNow);

        //ACT
        var expire = order.Value.Expire(DateTime.UtcNow);

        bool isReturningErrorAlreadyCompleted = expire
            .Errors
            .Any(g => g.Code == "Order.StatusAlreadyExpired");

        //Assert
        Assert.True(isReturningErrorAlreadyCompleted);
    }

    [Fact]
    public void Expire_Should_RaiseOrderExpiredDomainEvent_WhenOrderWasSuccessfulyExpired()
    {
        Item item = Item.Create(
            Guid.NewGuid(),
            "Item name",
            Guid.NewGuid(),
            100.32m,
            100,
            DateTime.UtcNow);

        ErrorOr<Order> order = Order.Place(
            item.Id,
            Guid.NewGuid(),
            DateTime.UtcNow,
            2,
            item.InStock,
            item.Price * 2,
            item.StockStatus);

        order.Value.Expire(DateTime.UtcNow);

        bool raisedOrderExpiredDomainEvent = order
            .Value
            .DomainEvents
            .Any(s => s.GetType() == typeof(OrderExpiredDomainEvent));

        Assert.True(raisedOrderExpiredDomainEvent);
    }

    [Fact]
    public void Pay_Should_ReturnAnErrorCannotPayedWhenStatusIsExpired_WhenOrderStatusIsExpired()
    {
        Item item = Item.Create(
            Guid.NewGuid(),
            "Item name",
            Guid.NewGuid(),
            100.32m,
            100,
            DateTime.UtcNow);

        ErrorOr<Order> order = Order.Place(
            item.Id,
            Guid.NewGuid(),
            DateTime.UtcNow,
            2,
            item.InStock,
            item.Price * 2,
            item.StockStatus);

        order.Value.Expire(DateTime.UtcNow);

        var pay = order.Value.Pay(DateTime.UtcNow, item.InStock, item.StockStatus);

        bool isReturningErrorCannotPayedWhenStatusIsExpired = pay
            .Errors
            .Any(d => d.Code == "Order.StatusIsExpired");

        Assert.True(isReturningErrorCannotPayedWhenStatusIsExpired);
    }

    [Fact]
    public void Pay_Should_ReturnAnErrorCannotBePayedWhenOrderStatusIsNotConfirmed_WhenOrderStatusIsNotConfirmed()
    {
        Item item = Item.Create(
            Guid.NewGuid(),
            "Item name",
            Guid.NewGuid(),
            100.32m,
            100,
            DateTime.UtcNow);

        ErrorOr<Order> order = Order.Place(
            item.Id,
            Guid.NewGuid(),
            DateTime.UtcNow,
            2,
            item.InStock,
            item.Price * 2,
            item.StockStatus);

        var pay = order.Value.Pay(DateTime.UtcNow, item.InStock, item.StockStatus);

        bool isReturningErrorCannotBePayedWhenOrderStatusIsNotConfirmed = pay
            .Errors
            .Any(v => v.Code == "Order.StatusIsNotConfirmed");

        Assert.True(isReturningErrorCannotBePayedWhenOrderStatusIsNotConfirmed);
    }

    [Fact]
    public void Pay_Should_RaiseOrderPayedDomainEvent_WhenOrderIsPayedSuccessfully()
    {
        Item item = Item.Create(
            Guid.NewGuid(),
            "Item name",
            Guid.NewGuid(),
            100.32m,
            100,
            DateTime.UtcNow);

        ErrorOr<Order> order = Order.Place(
            item.Id,
            Guid.NewGuid(),
            DateTime.UtcNow,
            2,
            item.InStock,
            item.Price * 2,
            item.StockStatus);

        order.Value.Confirm(DateTime.UtcNow);
        order.Value.Pay(DateTime.UtcNow, item.InStock, item.StockStatus);

        bool raisedOrderPayedDomainEvent = order
            .Value
            .DomainEvents
            .Any(x => x.GetType() == typeof(OrderPayedDomainEvent));
    }

    [Fact]
    public void Complete_Should_ReturnAnErrorOrderStatusIsNotPayed_WhenOrderStatusIsNotPayed()
    {
        Item item = Item.Create(
             Guid.NewGuid(),
             "Item name",
             Guid.NewGuid(),
             100.32m,
             100,
             DateTime.UtcNow);

        ErrorOr<Order> order = Order.Place(
            item.Id,
            Guid.NewGuid(),
            DateTime.UtcNow,
            2,
            item.InStock,
            item.Price * 2,
            item.StockStatus);

        order.Value.Confirm(DateTime.UtcNow);
        var complete = order.Value.Complete(DateTime.UtcNow);

        bool isReturningErrorOrderStatusIsNotPayed = complete
            .Errors
            .Any(c => c.Code == "Order.StatusIsNotPayed");

        Assert.True(isReturningErrorOrderStatusIsNotPayed);
    }

    [Fact]
    public void Complete_Should_RaiseOrderCompletedDomainEvent_WhenOrderIsSuccessfullyCompleted()
    {
        Item item = Item.Create(
             Guid.NewGuid(),
             "Item name",
             Guid.NewGuid(),
             100.32m,
             100,
             DateTime.UtcNow);

        ErrorOr<Order> order = Order.Place(
            item.Id,
            Guid.NewGuid(),
            DateTime.UtcNow,
            2,
            item.InStock,
            item.Price * 2,
            item.StockStatus);

        order.Value.Confirm(DateTime.UtcNow);
        order.Value.Pay(DateTime.UtcNow, item.InStock, item.StockStatus);

        order.Value.Complete(DateTime.UtcNow);

        bool raisedOrderCompletedDomainEvent = order
            .Value
            .DomainEvents
            .Any(f => f.GetType() == typeof(OrderCompletedDomainEvent));
    }
}
