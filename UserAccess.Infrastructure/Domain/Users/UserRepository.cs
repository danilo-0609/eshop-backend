using Microsoft.EntityFrameworkCore;
using UserAccess.Domain.Users;

namespace UserAccess.Infrastructure.Domain.Users;

internal sealed class UserRepository : IUserRepository
{
    private readonly UserAccessDbContext _dbContext;

    public UserRepository(UserAccessDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(User user)
    {
        await InsertUser(user);
        await InsertRoles(user);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbContext
            .Users
            .Where(r => r.Email == email)
            .SingleOrDefaultAsync();
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
            .Database
            .ExecuteSqlRawAsync(
            "UPDATE users.Users " +
            "SET Login = {0}, " +
            "Password = {1}, " +
            "Email = {2}, " +
            "IsActive = {3}, " +
            "FirstName = {4}, " +
            "LastName = {5}, " +
            "Name = {6}, " +
            "Address = {7}, " +
            "CreatedDateTime = {8}, " +
            "UpdatedDateTime = {9}  " +
            "WHERE UserId = {10}",
            user.Login,
            user.Password.Value,
            user.Email,
            user.IsActive,
            user.FirstName,
            user.LastName,
            user.Name,
            user.Address,
            user.CreatedDateTime,
            user.UpdatedDateTime!,
            user.Id.Value);
    }

    private async Task InsertRoles(User user)
    {
        foreach (var role in user.Roles)
        {
            await _dbContext.Database.ExecuteSqlRawAsync(
                $"INSERT INTO[Eshop.Db].[users].[UsersRoles] (UserId, RoleId)" +
                "VALUES({0}, {1})",
                user.Id.Value, role.RoleId);
        }
    }

    private async Task InsertUser(User user)
    {
        await _dbContext.Database.ExecuteSqlRawAsync(
        "INSERT INTO users.Users (UserId, " +
        "Login, " +
        "Password, " +
        "Email, " +
        "IsActive, " +
        "FirstName, " +
        "LastName, " +
        "Name, " +
        "Address, " +
        "CreatedDateTime, " +
        "UpdatedDateTime) " +
        "VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10})",
        user.Id.Value,
        user.Login,
        user.Password.Value,
        user.Email,
        user.IsActive,
        user.FirstName,
        user.LastName,
        user.Name,
        user.Address,
        user.CreatedDateTime,
        user.UpdatedDateTime);
    }
}
