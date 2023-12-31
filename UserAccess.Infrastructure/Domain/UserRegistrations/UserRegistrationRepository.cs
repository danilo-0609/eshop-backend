using Microsoft.EntityFrameworkCore;
using UserAccess.Domain.UserRegistrations;

namespace UserAccess.Infrastructure.Domain.UserRegistrations;

public sealed class UserRegistrationRepository : IUserRegistrationRepository, IUsersCounter
{
    private readonly UserAccessDbContext _dbContext;

    public UserRegistrationRepository(UserAccessDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(UserRegistration userRegistration)
    {
        await _dbContext
            .UserRegistrations
            .AddAsync(userRegistration);
    }

    public int CountUsersWithLogin(string login)
    {
        return _dbContext
            .UserRegistrations
            .Where(d => d.Login == login)
            .Count();
    }

    public async Task<UserRegistration?> GetByIdAsync(UserRegistrationId userRegistrationId)
    {
        return await _dbContext
            .UserRegistrations
            .Where(x => x.Id == userRegistrationId)
            .SingleOrDefaultAsync();
    }
}
