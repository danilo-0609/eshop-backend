namespace Shopping.Domain.Buying;

public interface IBuyRepository
{
    Task AddAsync(Buy buy);
}
