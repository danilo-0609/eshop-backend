using Microsoft.EntityFrameworkCore;
using Shopping.Domain.Orders;

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
        await _dbContext
            .Orders
            .Where(d => d.Id == order.Id)
            .ExecuteUpdateAsync(setters =>
            setters
                .SetProperty(d => d.Id, order.Id)
                .SetProperty(d => d.CustomerId, order.CustomerId)
                .SetProperty(d => d.ItemId, order.ItemId)
                .SetProperty(d => d.AmountOfItems, order.AmountOfItems)
                .SetProperty(d => d.TotalMoneyAmount, order.TotalMoneyAmount)
                .SetProperty(d => d.OrderStatus, order.OrderStatus)
                .SetProperty(d => d.PlacedOn, order.PlacedOn)
                .SetProperty(d => d.ConfirmedOn, order.ConfirmedOn)
                .SetProperty(d => d.ExpiredOn, order.ExpiredOn)
                .SetProperty(d => d.PayedOn, order.PayedOn)
                .SetProperty(d => d.CompletedOn, order.CompletedOn));
    }
}
