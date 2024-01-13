namespace Shopping.Domain.Items;

public interface IItemRepository
{
    Task AddAsync(Item item);

    Task<Item?> GetByIdAsync(ItemId id);
}
