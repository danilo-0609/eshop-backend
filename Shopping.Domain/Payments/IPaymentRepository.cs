namespace Shopping.Domain.Payments;

public interface IPaymentRepository
{
    Task AddAsync(Payment payment);
}
