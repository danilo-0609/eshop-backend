﻿using Microsoft.EntityFrameworkCore;
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