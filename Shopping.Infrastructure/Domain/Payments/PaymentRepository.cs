using Shopping.Domain.Payments;

namespace Shopping.Infrastructure.Domain.Payments;

internal sealed class PaymentRepository : IPaymentRepository
{
    private readonly ShoppingDbContext _dbContext;

    public PaymentRepository(ShoppingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Payment payment)
    {
        await _dbContext
            .Payments
            .AddAsync(payment);
    }
}
