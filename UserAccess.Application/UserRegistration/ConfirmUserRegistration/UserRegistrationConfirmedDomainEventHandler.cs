using BuildingBlocks.Application.Events;
using Microsoft.Extensions.Logging;
using UserAccess.Domain.UserRegistrations;
using UserAccess.Domain.UserRegistrations.Events;
using UserAccess.Domain.Users;

namespace UserAccess.Application.UserRegistration.ConfirmUserRegistration;

internal sealed class UserRegistrationConfirmedDomainEventHandler : IDomainEventHandler<UserRegistrationConfirmedDomainEvent>
{
    private readonly IUserRegistrationRepository _userRegistrationRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserRegistrationConfirmedDomainEventHandler> _logger;

    public UserRegistrationConfirmedDomainEventHandler(IUserRegistrationRepository userRegistrationRepository, IUserRepository userRepository, ILogger<UserRegistrationConfirmedDomainEventHandler> logger)
    {
        _userRegistrationRepository = userRegistrationRepository;
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task Handle(UserRegistrationConfirmedDomainEvent notification, CancellationToken cancellationToken)
    {
        var userRegistration = await _userRegistrationRepository.GetByIdAsync(notification.Id);

        var user = userRegistration!.CreateUser();

        if (user.IsError)
        {
            _logger.LogError("User creation failed: {@RequestName} @{Error} @{OcurredOn}",
                typeof(UserRegistrationConfirmedDomainEventHandler).Name,
                "UserRegistrationErrors.RegistrationIsNotConfirmed",
                DateTime.UtcNow);

            return;
        }

        await _userRepository.AddAsync(user.Value);
    }
}