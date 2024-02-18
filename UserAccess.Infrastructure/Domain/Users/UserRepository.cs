using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using UserAccess.Domain;
using UserAccess.Domain.Users;
using UserAccess.Infrastructure.Outbox;

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
        await InsertDomainEvent(user);
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

    public async Task RemoveAsync(User user)
    {
        await _dbContext
            .Users
            .Where(d => d.Id == user.Id)
            .ExecuteDeleteAsync();
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
        SqlParameter updatedDateTime = user.UpdatedDateTime != null ?
    new SqlParameter("@UpdatedDateTime", user.UpdatedDateTime) :
    new SqlParameter("@UpdatedDateTime", DBNull.Value);

        await _dbContext
            .Database
            .ExecuteSqlRawAsync(
            @"UPDATE users.Users " +
            "SET Login = @Login, " +
            "Password = @Password, " +
            "Email = @Email, " +
            "IsActive = @IsActive, " +
            "FirstName = @FirstName, " +
            "LastName = @LastName, " +
            "Name = @Name, " +
            "ProfileImageName = @ProfileImageName",
            "Address = @Address, " +
            "CreatedDateTime = @CreatedDateTime, " +
            "UpdatedDateTime = @UpdatedDateTime " +
            "WHERE UserId = @UserId",
            new SqlParameter("@Login", user.Login),
            new SqlParameter("@Password", user.Password.Value),
            new SqlParameter("@Email", user.Email),
            new SqlParameter("@IsActive", user.IsActive),
            new SqlParameter("@FirstName", user.FirstName),
            new SqlParameter("@LastName", user.LastName),
            new SqlParameter("@Name", user.Name),
            new SqlParameter("@ProfileImageName", user.ProfileImageName),
            new SqlParameter("@Address", user.Address),
            new SqlParameter("@CreatedDateTime", user.CreatedDateTime),
            updatedDateTime,
            new SqlParameter("@UserId", user.Id.Value));
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
            "CreatedDateTime) " +
            "VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9})",
            user.Id.Value,
            user.Login,
            user.Password.Value,
            user.Email,
            user.IsActive,
            user.FirstName,
            user.LastName,
            user.Name,
            user.Address,
            user.CreatedDateTime);
    }

    private async Task InsertRoles(User user)
    {
        foreach (Role role in user.Roles)
        {
            await _dbContext.Database.ExecuteSqlRawAsync(
                $"INSERT INTO[Eshop.Db].[users].[UsersRoles] (UserId, RoleId)" +
                "VALUES({0}, {1})",
                user.Id.Value, role.RoleId);
        }
    }

    public async Task AddRole(Guid userId, Role role)
    {
        await _dbContext.Database.ExecuteSqlRawAsync(
            $"INSERT INTO[Eshop.Db].[users].[UsersRoles] (UserId, RoleId)" +
            "VALUES({0}, {1})",
            userId, role.RoleId);
    }

    public async Task<List<Role>> GetRolesAsync(Guid userId)
    {
        return await _dbContext.Roles.FromSqlRaw(
            """
            SELECT TOP (3)
            	r.RoleId,
            	r.RoleCode
            FROM [Eshop.Db].dbo.Roles r
            INNER JOIN users.UsersRoles ur ON r.RoleId = ur.RoleId
            INNER JOIN users.Users u ON ur.UserId = u.UserId
            WHERE u.UserId = {0}            
            """,
            userId).ToListAsync();
    }

    private async Task InsertDomainEvent(User user)
    {
        var domainEvents = user.GetDomainEvents();

        List<UserAccessOutboxMessage> outboxMessages = domainEvents
        .Select(domainEvent => new UserAccessOutboxMessage
        {
            Id = domainEvent.DomainEventId,
            Type = domainEvent.GetType().Name,
            OcurredOnUtc = domainEvent.OcurredOn,
            Content = JsonConvert.SerializeObject(
            domainEvent,
            new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            })
        }).ToList();

        user.ClearDomainEvents();

        await _dbContext.UserAccessOutboxMessages.AddRangeAsync(outboxMessages);
    }
}