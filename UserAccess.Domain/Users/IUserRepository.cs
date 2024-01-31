namespace UserAccess.Domain.Users;

public interface IUserRepository
{
    Task AddAsync(User user);

    Task UpdateAsync(User user);

    Task<User?> GetByIdAsync(UserId userId);

    Task<bool> IsEmailUniqueAsync(string email);

    Task<bool> IsLoginUniqueAsync(string login);

    Task<User?> GetByEmailAsync(string email);

    Task RemoveAsync(User user);

    Task AddRole(Guid userId, Role role);

    Task<List<Role>> GetRolesAsync(Guid userId);
}