namespace Shopping.Application.Common;

public interface IShoppingUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
