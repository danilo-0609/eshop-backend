using Catalog.IntegrationEvents;
using MassTransit;
using Microsoft.Extensions.Logging;
using UserAccess.Domain;
using UserAccess.Domain.Users;

namespace UserAccess.Application.IntegrationEventCostumers;

public sealed class ProductPublishedIntegrationEventConsumer : IConsumer<ProductPublishedIntegrationEvent>
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<ProductPublishedIntegrationEventConsumer> _logger;

    public ProductPublishedIntegrationEventConsumer(IUserRepository userRepository, ILogger<ProductPublishedIntegrationEventConsumer> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ProductPublishedIntegrationEvent> context)
    {
        _logger.LogInformation("Start consuming: {Name}, {DateTime}",
            context.GetType().FullName,
            DateTime.UtcNow);

        var userId = context.Message.SellerId;

        var roles = await _userRepository.GetRolesAsync(userId);

        if (roles!.Any(r => r.RoleCode != Role.Seller.RoleCode))
        {
            await _userRepository.AddRole(userId, Role.Seller);
        }

        _logger.LogInformation("Consuming finished: {Name}, {DateTime}",
            context.GetType().FullName,
            DateTime.UtcNow);
    }
}
