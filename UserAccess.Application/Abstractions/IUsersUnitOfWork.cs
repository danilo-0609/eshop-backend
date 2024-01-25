namespace UserAccess.Application.Abstractions;
public interface IUsersUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}