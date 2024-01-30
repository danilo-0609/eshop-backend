using Catalog.IntegrationEvents;
using MassTransit;
using MediatR;
using UserAccess.Domain;
using UserAccess.Domain.Users;

namespace UserAccess.Application.IntegrationEventCostumers;

public sealed class ProductPublishedIntegrationEventConsumer : IConsumer<ProductPublishedIntegrationEvent>
{
    private readonly IUserRepository _userRepository;

    public ProductPublishedIntegrationEventConsumer(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Consume(ConsumeContext<ProductPublishedIntegrationEvent> context)
    {
        var userId = context.Message.SellerId;

        var user = await _userRepository.GetByIdAsync(UserId.Create(userId));

        if (!user!.Roles.Any(r => r.RoleCode == "Seller"))
        {
            await _userRepository.AddRole(user.Id.Value, Role.Seller);
        }
    }
}
