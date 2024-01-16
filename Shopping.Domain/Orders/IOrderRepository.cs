namespace Shopping.Domain.Orders;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(OrderId orderId);

    Task AddAsync(Order order);

    Task UpdateAsync(Order order);
}
