using Microsoft.EntityFrameworkCore;
using UserAccess.Domain.Users;

namespace UserAccess.Infrastructure.Domain.Users;

public sealed class UserRepository : IUserRepository
{
    private readonly UserAccessDbContext _dbContext;

    public UserRepository(UserAccessDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(User user)
    {
        await _dbContext
            .Users
            .AddAsync(user);
    }

    public async Task<User?> GetByIdAsync(UserId userId)
    {
        return await _dbContext
            .Users
            .Where(r => r.Id == userId)
            .SingleOrDefaultAsync();
    }

    public async Task<bool> IsEmailUniqueAsync(string email)
    {
        return !await _dbContext
            .Users
            .AnyAsync(r => r.Email == email);
    }

    public async Task<bool> IsLoginUniqueAsync(string login)
    {
        return !await _dbContext
            .Users
            .AnyAsync(r => r.Login == login);
    }

    public async Task UpdateAsync(User user)
    {
        await _dbContext
            .Users
            .Where(r => r.Id == user.Id)
            .ExecuteUpdateAsync(setters =>
              setters
               .SetProperty(x => x.Id, user.Id)
               .SetProperty(x => x.Login, user.Login)
               .SetProperty(x => x.Password, user.Password)
               .SetProperty(x => x.Email, user.Email)
               .SetProperty(x => x.IsActive, user.IsActive)
               .SetProperty(x => x.FirstName, user.FirstName)
               .SetProperty(x => x.LastName, user.LastName)
               .SetProperty(x => x.Name, user.Name)
               .SetProperty(x => x.Address, user.Address)
               .SetProperty(x => x.Roles, user.Roles)
               .SetProperty(x => x.CreatedDateTime, user.CreatedDateTime)
               .SetProperty(x => x.UpdatedDateTime, user.UpdatedDateTime));
    }
}
