using Catalog.Entities;


namespace Catalog.Repositories
{
    public class InMemItemsRepository : IItemsRepository
    {
        private readonly List<Item> items = new(){ // Data for the database
            // This is a new way of instancing values called 'Target typed new expression'
            new Item { Id = Guid.NewGuid(), Name = "Potion", Price = 9, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Iron Sword", Price = 20, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Bronze Shield", Price = 18, CreatedDate = DateTimeOffset.UtcNow }
        };

        public IEnumerable<Item> GetItems()
        { // We can call this from anywhere to get all our data
            return items;
        }

        public Item? GetItem(Guid id)
        { // Use this anywhere to get an item via is Guid
            return items.Where(item => item.Id == id).SingleOrDefault();
            // SingleOrDefault() Returns the item or null
        }
    }
}