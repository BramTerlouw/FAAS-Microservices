using Microsoft.Azure.Cosmos;
using TriggerChain.Models;
using Container = Microsoft.Azure.Cosmos.Container;

namespace TriggerChain.Services
{
    public class CosmosDbService : ICosmosDbService<Product>
    {
        private Container _container;

        public CosmosDbService(
            CosmosClient cosmosDbClient,
            string databaseName,
            string containerName)
        {
            _container = cosmosDbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddAsync(Product item)
        {
            await _container.CreateItemAsync(item, new PartitionKey(item.ProductID));
        }

        public async Task DeleteAsync(string id)
        {
            await _container.DeleteItemAsync<Product>(id, new PartitionKey(id));
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
                Console.WriteLine(ex.Message);
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
            await _container.UpsertItemAsync(UpdateItem, new PartitionKey(id));
        }
    }
}
