using BuildingBlocks.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shopping.Domain.Orders;
using Shopping.Infrastructure.Outbox;

namespace Shopping.Infrastructure.Domain.Orders;

internal sealed class OrderRepository : IOrderRepository
{
    private readonly ShoppingDbContext _dbContext;

    public OrderRepository(ShoppingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Order order)
    {
        await _dbContext
            .Orders
            .AddAsync(order);
    }

    public async Task<Order?> GetByIdAsync(OrderId orderId)
    {
        return await _dbContext
            .Orders
            .Where(d => d.Id == orderId)
            .SingleOrDefaultAsync();
    }

    public async Task UpdateAsync(Order order)
    {
        await UpsertAsync(order);
        await InsertDomainEvents(order);
    }

    private async Task UpsertAsync(Order order)
    {
        SqlParameter expiredOnParam = order.ExpiredOn != null ?
            new SqlParameter("@ExpireOn", order.ExpiredOn) :
            new SqlParameter("@ExpireOn", DBNull.Value);

        SqlParameter payedOnParam = order.PayedOn != null ?
            new SqlParameter("@PayedOn", order.PayedOn) :
            new SqlParameter("@PayedOn", DBNull.Value);

        SqlParameter confirmedOnParam = order.ConfirmedOn != null ?
            new SqlParameter("@ConfirmedOn", order.ConfirmedOn) :
            new SqlParameter("@ConfirmedOn", DBNull.Value);

        SqlParameter completeddOnParam = order.CompletedOn != null ?
            new SqlParameter("@CompletedOn", order.CompletedOn) :
            new SqlParameter("@CompletedOn", DBNull.Value);

        await _dbContext
            .Database
            .ExecuteSqlRawAsync(
            @"
            UPDATE [Eshop.Db].[shopping].[Orders]
            SET OrderId = @OrderId,
                CustomerId = @CustomerId,
                ItemId = @ItemId,
                AmountOfItems = @AmountOfItems,
                TotalMoneyAmount = @TotalMoneyAmount,
                OrderStatus = @OrderStatus,
                PlacedOn = @PlacedOn,
                ConfirmedOn = @ConfirmedOn,
                ExpireOn = @ExpireOn,
                PayedOn = @PayedOn,
                CompletedOn = @CompletedOn
            WHERE OrderId = @OrderId
            ",
            new SqlParameter("@OrderId", order.Id.Value),
            new SqlParameter("@CustomerId", order.CustomerId),
            new SqlParameter("@ItemId", order.ItemId.Value),
            new SqlParameter("@AmountOfItems", order.AmountOfItems),
            new SqlParameter("@TotalMoneyAmount", order.TotalMoneyAmount),
            new SqlParameter("@OrderStatus", order.OrderStatus.Value),
            new SqlParameter("@PlacedOn", order.PlacedOn),
            confirmedOnParam,
            expiredOnParam,
            payedOnParam,
            completeddOnParam);
    }

    private async Task InsertDomainEvents(Order order)
    {
        var domainEvents = order.GetDomainEvents();

        List<ShoppingOutboxMessage> outboxMessages = domainEvents
            .Select(domainEvent => new ShoppingOutboxMessage
            {
                Id = domainEvent.DomainEventId,
                Type = domainEvent.GetType().Name,
                OcurredOnUtc = domainEvent.OcurredOn,
                Content = JsonConvert.SerializeObject(
                    domainEvent,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    })
            }).ToList();

        order.ClearDomainEvents();

        await _dbContext
            .ShoppingOutboxMessages
            .AddRangeAsync(outboxMessages);
    }
}
