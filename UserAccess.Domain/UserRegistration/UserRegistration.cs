using BuildingBlocks.Domain;
using UserAccess.Domain.Common;
using UserAccess.Domain.UserRegistrations.Rules;
using ErrorOr;
using UserAccess.Domain.UserRegistrations.Events;
using UserAccess.Domain.Users;
using MediatR;

namespace UserAccess.Domain.UserRegistrations;
public sealed class UserRegistration : AggregateRoot<UserRegistrationId, Guid>
{
    public new UserRegistrationId Id { get; private set; }

    public string Login { get; private set; }

    public Password Password { get; private set; }

    public string Email { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public string Name { get; private set; }

    public string Address { get; private set; }

    public DateTime RegisteredDate { get; private set; }

    public UserRegistrationStatus Status { get; private set; }

    public DateTime? ConfirmedDate { get; private set; }

    public static ErrorOr<UserRegistration> RegisterNewUser(
        string login, 
        string password,
        string email,
        string firstName,
        string lastName,
        string address,
        IUsersCounter usersCounter,
        DateTime registeredDate)
    {
        Password hash = Password.Create(password);

        UserRegistration userRegistration = new UserRegistration(
            UserRegistrationId.CreateUnique(),
            login,
            hash,
            email,
            firstName,
            lastName,
            $"{firstName} {lastName}",
            address,
            registeredDate,
            UserRegistrationStatus.WaitingForConfirmation,
            null);

        var isLoginUnique = userRegistration.CheckRule(new UserLoginMustBeUniqueRule(login, usersCounter));

        if (isLoginUnique.IsError)
        {
            return isLoginUnique.FirstError;
        }

        userRegistration.Raise(new NewUserRegisteredDomainEvent(
            Guid.NewGuid(),
            userRegistration.Id,
            login,
            email,
            firstName,
            lastName,
            $"{firstName} {lastName}",
            registeredDate));

        return userRegistration;
    }

    public ErrorOr<User> CreateUser()
    {
        var isRegisteredSuccessfully = CheckRule(new UserCannotBeCreatedWhenRegistrationIsNotConfirmedRule(Status)); 

        if (isRegisteredSuccessfully.IsError)
        {
            return isRegisteredSuccessfully.FirstError;
        }

        return User.CreateUserFromRegistration(
            Id,
            Login,
            Password,
            Email,
            FirstName,
            LastName,
            Name,
            Address,
            DateTime.UtcNow);
    }

    public ErrorOr<Unit> Confirm()
    {
        var confirmedMoreThanOnce = CheckRule(new UserRegistrationCannotBeConfirmedMoreThanOnceRule(Status));

        if (confirmedMoreThanOnce.IsError)
        {
            return confirmedMoreThanOnce.FirstError;
        } 

        var confirmedAfterExpiration = CheckRule(new UserRegistrationCannotBeConfirmedAfterExpirationRule(Status));

        if (confirmedAfterExpiration.IsError)
        {
            return confirmedAfterExpiration.FirstError;
        }

        Status = UserRegistrationStatus.Confirmed;
        ConfirmedDate = DateTime.UtcNow;

        Raise(new UserRegistrationConfirmedDomainEvent(
            Guid.NewGuid(),
            Id,
            DateTime.UtcNow));

        return Unit.Value;
    }

    public ErrorOr<Unit> Expired()
    {
        var expiredMoreThanOnce = CheckRule(new UserRegistrationCannotBeExpiredMoreThanOnceRule(Status));

        if (expiredMoreThanOnce.IsError)
        {
            return expiredMoreThanOnce.FirstError;
        }

        Status = UserRegistrationStatus.Expired;

        Raise(new UserRegistrationExpiredDomainEvent(
            Guid.NewGuid(),
            Id,
            DateTime.UtcNow));

        return Unit.Value;
    }

    private UserRegistration(
        UserRegistrationId id,
        string login,
        Password password,
        string email,
        string firstName,
        string lastName,
        string name,
        string address,
        DateTime registeredDate,
        UserRegistrationStatus userRegistrationStatus,
        DateTime? confirmedDate)
        : base(id)
    {
        Id = id;
        Login = login;
        Password = password;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Name = name;
        RegisteredDate = registeredDate;
        Status = userRegistrationStatus;
        ConfirmedDate = confirmedDate;
        Address = address;
    }

    private UserRegistration(){}
}