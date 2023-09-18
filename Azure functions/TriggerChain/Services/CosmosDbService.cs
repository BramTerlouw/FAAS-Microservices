using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using TriggerChain.Models;
using Container = Microsoft.Azure.Cosmos.Container;

namespace TriggerChain.Services
{
    public class CosmosDbService : ICosmosDbService<Product>
    {
        private Container _container;
        private readonly ILogger _logger;

        public CosmosDbService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<CosmosDbService>();
            var account = "https://localhost:8081";
            var key = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";

            var cosmosDbClient = new CosmosClient(account, key);

            var database = cosmosDbClient.GetDatabase("orderDB");
            _container = database.GetContainer("orders");
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

        public async Task DeleteAsync(string id)
        {
            try
            {
                await _container.DeleteItemAsync<Product>(id, new PartitionKey(id));
            } 
            catch (Exception)
            {
                throw new NotImplementedException();
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

        public async Task UpdateAsync(string id, Product UpdateItem)
        {
            try
            {
                await _container.UpsertItemAsync(UpdateItem, new PartitionKey(id));
            }
            catch (Exception) 
            {
                throw new NotImplementedException();
            }
        }
    }
}
