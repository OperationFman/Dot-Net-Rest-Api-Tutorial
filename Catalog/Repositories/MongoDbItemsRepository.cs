using Catalog.Entities;
using MongoDB.Driver;

namespace Catalog.Repositories {
    public class MongoDbItemsRepository : IItemsRepository
    {
        private const string databaseName = "catalogue"; // Reusable name for our one database
        private const string collectionName = "items"; // Reusable name for our one collection
        private readonly IMongoCollection<Item> itemsCollection; // Collections are how MongoDB stores the .json files
        
        public MongoDbItemsRepository(IMongoClient mongoClient) { // Constructor
            IMongoDatabase database = mongoClient.GetDatabase(databaseName); // Instance our database
            itemsCollection = database.GetCollection<Item>(collectionName); // Instance our collection
        }
        
        public void CreateItem(Item item)
        {
            itemsCollection.InsertOne(item);
        }

        public void DeleteItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public Item? GetItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Item> GetItems()
        {
            throw new NotImplementedException();
        }

        public void UpdateItem(Item item)
        {
            throw new NotImplementedException();
        }
    }
}