using UserAccess.Domain.Users;

namespace UserAccess.Application.Abstractions;
public interface IJwtProvider
{
    string Generate(User user);
}
