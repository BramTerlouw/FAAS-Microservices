using System;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TriggerChain.Models;

namespace TriggerChain.Controllers
{
    public class OrderedQueue
    {
        private readonly ILogger _logger;

        public OrderedQueue(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ReceiveHttp>();
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
                //_cosmosDbService.AddAsync(product);
            }
        }
    }
}
