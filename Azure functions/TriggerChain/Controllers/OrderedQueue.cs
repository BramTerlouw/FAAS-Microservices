using System;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TriggerChain.Models;
using TriggerChain.Services;

namespace TriggerChain.Controllers
{
    public class OrderedQueue
    {
        private readonly ILogger _logger;
        private readonly ICosmosDbService<Product> _cosmosDbService;

        public OrderedQueue(ILoggerFactory loggerFactory, ICosmosDbService<Product> cosmosDbService)
        {
            _logger = loggerFactory.CreateLogger<OrderedQueue>();
            _cosmosDbService = cosmosDbService;
        }

        [Function(nameof(OrderedQueue))]
        public void Run([QueueTrigger("orderedproductqueue", Connection = "default")] string message)
        {
            _logger.LogInformation("C# Queue trigger function processed a request.");

            // Deserialize message back into a product
            Product? product = JsonConvert.DeserializeObject<Product>(message);

            if (product != null) 
            {
                _logger.LogInformation("Ordered product is added to CosmosDB");
                _cosmosDbService.AddAsync(product);
            }
        }
    }
}
