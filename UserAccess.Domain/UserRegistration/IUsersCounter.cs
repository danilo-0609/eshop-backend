namespace UserAccess.Domain.UserRegistrations;
public interface IUsersCounter
{
    int CountUsersWithLogin(string login);
}
