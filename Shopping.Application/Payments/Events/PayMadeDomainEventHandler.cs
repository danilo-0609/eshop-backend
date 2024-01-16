using BuildingBlocks.Application.Events;
using Shopping.Domain.Payments;
using Shopping.Domain.Payments.DomainEvents;

namespace Shopping.Application.Payments.Events;

internal sealed class PayMadeDomainEventHandler : IDomainEventHandler<PayMadeDomainEvent>
{
    private readonly IPaymentRepository _paymentRepository;

    public PayMadeDomainEventHandler(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task Handle(PayMadeDomainEvent notification, CancellationToken cancellationToken)
    {
        Payment payment = Payment.Create(
            notification.PaymentId,
            notification.OrderId,
            notification.PayerId,
            notification.MoneyAmount,
            notification.OcurredOn);

        await _paymentRepository.AddAsync(payment);
    }
}
