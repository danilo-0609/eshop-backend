using BuildingBlocks.Application.Events;
using Shopping.Application.Common;
using Shopping.Domain.Payments;
using Shopping.Domain.Payments.DomainEvents;

namespace Shopping.Application.Payments.Events;

internal sealed class PayMadeDomainEventHandler : IDomainEventHandler<PayMadeDomainEvent>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IShoppingUnitOfWork _unitOfWork;

    public PayMadeDomainEventHandler(IPaymentRepository paymentRepository, IShoppingUnitOfWork unitOfWork)
    {
        _paymentRepository = paymentRepository;
        _unitOfWork = unitOfWork;
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

        await _unitOfWork.SaveChangesAsync();
    }
}
