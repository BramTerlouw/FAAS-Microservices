using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TriggerChain.Models;
using TriggerChain.Services.Interfaces;

namespace TriggerChain.Controllers
{
    public class ProductQueue
    {
        private readonly ILogger _logger;
        private readonly IQueueService _queueService;
        private readonly ICosmosDbService<Product> _productService;

        public ProductQueue(ILoggerFactory loggerFactory, IQueueService queueService, ICosmosDbService<Product> productService)
        {
            _logger = loggerFactory.CreateLogger<ProductQueue>();
            _queueService = queueService;
            _productService = productService;
        }

        [Function(nameof(ProductQueue))]
        public async Task Run([QueueTrigger("productqueue", Connection = "default")] QueueMessage message)
        {
            _logger.LogInformation("C# Queue trigger function processed a request.");
            
            // Deserialize message back into a product.
            Product? product = JsonConvert.DeserializeObject<Product>(message.Body.ToString());

            // Add product to cosmos db and new order to queue
            if (product != null)
            {
                Order order = new Order(1, "Order1", product, DateTime.Now);
                await _productService.AddAsync(product);
                await _queueService.SendQueueMessageAsync("orderedproductqueue", order);
            }
        }
    }
}
