using System;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage;
using Newtonsoft.Json;
using TriggerChain.Models;
using TriggerChain.Services;

namespace TriggerChain.Controllers
{
    public class ProductQueue
    {
        private readonly ILogger _logger;
        private readonly IQueueService _queueService;

        public ProductQueue(ILoggerFactory loggerFactory, IQueueService queueService)
        {
            _logger = loggerFactory.CreateLogger<ProductQueue>();
            _queueService = queueService;
        }

        [Function(nameof(ProductQueue))]
        public async Task Run([QueueTrigger("productqueue", Connection = "default")] string message)
        {
            _logger.LogInformation("C# Queue trigger function processed a request.");
            
            // Deserialize message back into a product
            Product? product = JsonConvert.DeserializeObject<Product>(message);

            // Change propertie isOrdered of product to true
            if (product != null)
            {
                product.isOrdered = true;
                await _queueService.SendQueueMessageAsync("orderedproductqueue", product);
            }
        }
    }
}
