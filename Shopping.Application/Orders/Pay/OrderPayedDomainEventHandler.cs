using BuildingBlocks.Application.Events;
using Shopping.Application.Common;
using Shopping.Domain.Orders;
using Shopping.Domain.Orders.Events;
using Shopping.Domain.Payments;
using Shopping.IntegrationEvents;

namespace Shopping.Application.Orders.Pay;

internal sealed class OrderPayedDomainEventHandler : IDomainEventHandler<OrderPayedDomainEvent>
{
    private readonly IShoppingEventBus _shoppingEventBus;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPaymentRepository _paymentRepository;

    public OrderPayedDomainEventHandler(IShoppingEventBus shoppingEventBus, IUnitOfWork unitOfWork, IPaymentRepository paymentRepository)
    {
        _shoppingEventBus = shoppingEventBus;
        _unitOfWork = unitOfWork;
        _paymentRepository = paymentRepository;
    }

    public async Task Handle(OrderPayedDomainEvent notification, CancellationToken cancellationToken)
    {
        await _shoppingEventBus.PublishAsync(new OrderPayedIntegrationEvent(
            notification.DomainEventId,
            notification.ItemId.Value,
            notification.OrderId.Value,
            notification.AmountOfProducts,
            DateTime.UtcNow));

        var payment = Payment.PayFromOrder(
            notification.OrderId,
            notification.MoneyAmount,
            notification.AmountOfProducts,
            notification.ItemId,
            notification.CustomerId,
            notification.ActualStock,
            notification.StockStatus);

        await _paymentRepository.AddAsync(payment.Value);

        await _unitOfWork.SaveChangesAsync();
    }
}
