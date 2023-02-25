using Catalog.Entities;

namespace Catalog.Repositories {
    public interface IItemsRepository
    {
        Task<Item?> GetItemAsync(Guid id); // Task == Async
        Task<IEnumerable<Item>> GetItemsAsync();

        Task CreateItemAsync(Item item); // Defaults to void

        Task UpdateItemAsync(Item item);

        Task DeleteItemAsync(Guid id);
    }
}
