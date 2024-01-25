using BuildingBlocks.Application.Events;
using UserAccess.Application.Abstractions;
using UserAccess.Domain.UserRegistrations;
using UserAccess.Domain.UserRegistrations.Events;
using UserAccess.Domain.Users;

namespace UserAccess.Application.UserRegistration.ConfirmUserRegistration;

internal sealed class UserRegistrationConfirmedDomainEventHandler : IDomainEventHandler<UserRegistrationConfirmedDomainEvent>
{
    private readonly IUserRegistrationRepository _userRegistrationRepository;
    private readonly IUserRepository _userRepository;

    public UserRegistrationConfirmedDomainEventHandler(IUserRegistrationRepository userRegistrationRepository, IUserRepository userRepository)
    {
        _userRegistrationRepository = userRegistrationRepository;
        _userRepository = userRepository;
    }

    public async Task Handle(UserRegistrationConfirmedDomainEvent notification, CancellationToken cancellationToken)
    {
        var userRegistration = await _userRegistrationRepository.GetByIdAsync(notification.Id);

        var user = userRegistration!.CreateUser();

        await _userRepository.AddAsync(user.Value);
    }
}