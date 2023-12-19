using Microsoft.EntityFrameworkCore;
using UserAccess.Domain.UserRegistrations;
using UserAccess.Domain.Users;

namespace UserAccess.Infrastructure;

internal interface IApplicationDbContext
{
    DbSet<User> Users { get; }

    DbSet<UserRegistration> UserRegistrations { get; }
}
