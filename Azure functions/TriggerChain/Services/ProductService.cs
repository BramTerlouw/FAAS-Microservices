using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using System.Configuration;
using TriggerChain.Models;
using TriggerChain.Services.Interfaces;
using Container = Microsoft.Azure.Cosmos.Container;

namespace TriggerChain.Services
{
    public class ProductService : ICosmosDbService<Product>
    {
        private Container _container;
        private readonly ILogger _logger;

        public ProductService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ProductService>();

            var key = ConfigurationManager.AppSettings["key"];
            var account = ConfigurationManager.AppSettings["account"];
            var cosmosDbClient = new CosmosClient(account, key);

            var database = cosmosDbClient.GetDatabase("productsDB");
            _container = database.GetContainer("products");
        }

        public async Task AddAsync(Product item)
        {
            try
            {
                await _container.CreateItemAsync(item, new PartitionKey(item.ProductID));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
            }
            
        }

        public async Task<Product> GetAsync(string id)
        {
            ItemResponse<Product> response = null;
            try
            {
                response = await _container.ReadItemAsync<Product>(id, new PartitionKey(id));
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                _logger.LogError($"Error: {ex.Message}");
            }
            return response;
        }

        public async Task<IEnumerable<Product>> GetMultipleAsync(string queryString)
        {
            var query = _container.GetItemQueryIterator<Product>(new QueryDefinition(queryString));

            var results = new List<Product>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }

            return results;
        }
    }
}
