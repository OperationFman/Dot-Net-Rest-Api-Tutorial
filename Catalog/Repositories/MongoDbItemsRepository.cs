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
        
        public void CreateItem(Item item)
        {
            itemsCollection.InsertOne(item);
        }

        public void DeleteItem(Guid id)
        {
            var filter = filterBuilder.Eq(item =>item.Id, id);
            itemsCollection.DeleteOne(filter);
        }

        public Item? GetItem(Guid id)
        {
            var filter = filterBuilder.Eq(item =>item.Id, id);
			return itemsCollection.Find(filter).SingleOrDefault();
        }

        public IEnumerable<Item> GetItems()
        {
            return itemsCollection.Find(new BsonDocument()).ToList();
        }

        public void UpdateItem(Item item)
        {
            var filter = filterBuilder.Eq(existingItem =>existingItem.Id, item.Id);
            itemsCollection.ReplaceOne(filter, item);
        }
    }
}