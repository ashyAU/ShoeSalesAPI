using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;
using ShoeSalesAPI.Models;

namespace ShoeSalesAPI.Services
{
    public class ShoeService
    {
        private readonly IMongoCollection<Shoe> _shoeCollection;

        public ShoeService(
            IOptions<ShoeStoreDatabaseSettings> shoeStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                shoeStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                shoeStoreDatabaseSettings.Value.DatabaseName);

            _shoeCollection = mongoDatabase.GetCollection<Shoe>(
                shoeStoreDatabaseSettings.Value.ShoesCollectionName);
        }
        /// <summary>
        /// Fetches all products based on a mongo query async task
        /// </summary>
        /// <returns>async list of all products</returns>
        public async Task<List<Shoe>> GetAllProducts()
        {
            return await _shoeCollection.Find(_ => true).ToListAsync();
        }

        /// <summary>
        /// Fetches price range based on a mongo query async task
        /// </summary>
        /// <param name="minPrice">Minimum price of the product filter</param>
        /// <param name="maxPrice">Maximum price of the product filter</param>
        /// <returns>async list of all products within the price range</returns>
        public async Task<List<Shoe>> GetShoesByPrice(double minPrice, double maxPrice)
        {
            return await _shoeCollection.Find(shoe => minPrice <= shoe.Price && shoe.Price <= maxPrice).ToListAsync();
        }

        /// <summary>
        /// Fetches only availible stock based on mongo query async task
        /// </summary>
        /// <returns>async list of all products that are currently in stock</returns>
        public async Task<List<Shoe>> GetShoesByAvailibility(bool available)
        {
            return await _shoeCollection.Find(shoe => shoe.isAvailable == available).ToListAsync();
        }


        public async Task<Shoe> UpdateProduct(int sku, Shoe updatedShoe)
        {
            var filter = Builders<Shoe>.Filter.Eq(s => s.SKU, sku);

            var update = Builders<Shoe>.Update.Set(s => s.ProductName, updatedShoe.ProductName)
                                             .Set(s => s.Price, updatedShoe.Price)
                                             .Set(s => s.isAvailable, updatedShoe.isAvailable)
                                             .Set(s => s.Description, updatedShoe.Description);

            var updateResult = await _shoeCollection.UpdateOneAsync(filter, update);

            var updatedProduct = await _shoeCollection.FindAsync(filter);
            return updatedProduct.FirstOrDefault();
        }

        public async Task<List<Shoe>?> DeleteProduct(int sku)
        {
            var filter = Builders<Shoe>.Filter.Eq(s => s.SKU, sku);
            var delete = _shoeCollection.DeleteOneAsync(filter);

            if (delete.Result.DeletedCount == 0)
            {
                return null;
            }
            return await _shoeCollection.Find(_ => true).ToListAsync();
        }







    }
}
