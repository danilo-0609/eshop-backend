using BuildingBlocks.Domain;
using UserAccess.Domain.Common;
using UserAccess.Domain.UserRegistrations;
using UserAccess.Domain.Users.Events;

namespace UserAccess.Domain.Users;

public sealed class User : AggregateRoot<UserId, Guid>
{   
    public new UserId Id { get; private set; }

    public string Login { get; private set; }

    public Password Password { get; private set; }

    public string Email { get; private set; }

    public bool IsActive { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public string Name { get; private set; }

    public string Address { get; private set; }

    public List<Role> Roles { get; private set; }

    public string ProfileImageName { get; private set; } = string.Empty;

    public DateTime CreatedDateTime { get; private set; }

    public DateTime? UpdatedDateTime { get; private set; } 

    public static User CreateAdmin(
        string login,
        string password,
        string email,
        string firstName,
        string lastName,
        string name,
        string address,
        DateTime createdDateTime)
    {
        User user = new User(
            UserId.CreateUnique(),
            login,
            Password.CreateUnique(password),
            email,
            true,
            firstName,
            lastName,
            name,
            address,
            new List<Role>
            {
                Role.Administrator
            },
            createdDateTime,
            null);

        user.Raise(new UserCreatedDomainEvent(
            Guid.NewGuid(),
            user.Id,
            createdDateTime));

        return user;
    }

    internal static User CreateUserFromRegistration(
        UserRegistrationId userRegistrationId,
        string login,
        Password password,
        string email,
        string firstName,
        string lastName,
        string name,
        string address,
        DateTime createdDateTime)
    {
        User user = new User(
            UserId.Create(userRegistrationId.Value),
            login,
            password,
            email,
            true,
            firstName,
            lastName,
            name,
            address,
            new List<Role>() 
            {
                Role.Customer
            },
            createdDateTime,
            null);

        user.Raise(new UserCreatedDomainEvent(
            Guid.NewGuid(),
            user.Id,
            createdDateTime));

        return user;
    }

    public static User Update(
        UserId id,
        string login,
        string password,
        string email,
        string firstName,
        string lastName,
        string address,
        List<Role> roles,
        DateTime createdDateTime,
        DateTime updatedDateTime,
        string profileImageName = "")
    {
        return new User(
            id,
            login,
            Password.Create(password),
            email,
            true,
            firstName,
            lastName,
            $"{firstName} {lastName}",
            address,
            roles,
            profileImageName,
            createdDateTime,
            updatedDateTime);
    }

    public void AddRole(Role role)
    {
        Roles.Add(role);
    }

    private User(
        UserId id,
        string login,
        Password password,
        string email,
        bool isActive,
        string firstName,
        string lastName,
        string name,
        string address,
        List<Role> roles,
        DateTime createdDateTime,
        DateTime? updatedDateTime)
        : base(id)
    {
        Id = id;
        Login = login;
        Password = password;
        Email = email;
        IsActive = isActive;
        FirstName = firstName;
        LastName = lastName;
        Name = name;

        Roles = roles;

        ProfileImageName = "";
        Address = address;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

    private User(
    UserId id,
    string login,
    Password password,
    string email,
    bool isActive,
    string firstName,
    string lastName,
    string name,
    string address,
    List<Role> roles,
    string profileImageName,
    DateTime createdDateTime,
    DateTime? updatedDateTime)
    : base(id)
    {
        Id = id;
        Login = login;
        Password = password;
        Email = email;
        IsActive = isActive;
        FirstName = firstName;
        LastName = lastName;
        Name = name;

        Roles = roles;

        ProfileImageName = profileImageName;
        Address = address;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

    private User() {}
}