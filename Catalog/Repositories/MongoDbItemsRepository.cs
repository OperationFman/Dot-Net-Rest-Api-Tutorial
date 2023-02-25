using Catalog.Entities;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Catalog.Repositories {
    public class MongoDbItemsRepository : IItemsRepository
    {
        private const string databaseName = "catalogue"; // Reusable name for our one database
        private const string collectionName = "items"; // Reusable name for our one collection
        private readonly IMongoCollection<Item> itemsCollection; // Collections are how MongoDB stores the .json files
        
        private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;

        // The below will make more sense later once everything is configured
        public MongoDbItemsRepository(IMongoClient mongoClient) { // Constructor
            IMongoDatabase database = mongoClient.GetDatabase(databaseName); // Instance our database
            itemsCollection = database.GetCollection<Item>(collectionName); // Instance our collection
        }
        
        public async Task CreateItemAsync(Item item)
        {
            await itemsCollection.InsertOneAsync(item);
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item =>item.Id, id);
            await itemsCollection.DeleteOneAsync(filter);
        }

        public async Task<Item?> GetItemAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item =>item.Id, id);
			return await itemsCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await itemsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateItemAsync(Item item)
        {
            var filter = filterBuilder.Eq(existingItem =>existingItem.Id, item.Id);
            await itemsCollection.ReplaceOneAsync(filter, item);
        }
    }
}