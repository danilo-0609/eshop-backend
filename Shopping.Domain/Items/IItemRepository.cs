namespace Shopping.Domain.Items;

public interface IItemRepository
{
    Task AddAsync(Item item);

    Task<Item?> GetByIdAsync(ItemId id);

    Task UpdateAsync(Item item);

    Task DeleteAsync(ItemId id);

    Guid? GetSellerIdAsync(ItemId itemId);
}
