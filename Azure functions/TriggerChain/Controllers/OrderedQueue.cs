using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using TriggerChain.Models;
using TriggerChain.Services.Interfaces;

namespace TriggerChain.Controllers
{
    public class OrderedQueue
    {
        private readonly ILogger _logger;
        private readonly ITableStorageService<Order> _orderService;

        public OrderedQueue(ILoggerFactory loggerFactory, ITableStorageService<Order> orderService)
        {
            _logger = loggerFactory.CreateLogger<OrderedQueue>();
            _orderService = orderService;
        }

        [Function(nameof(OrderedQueue))]
        public async Task RunAsync([QueueTrigger("orderedproductqueue", Connection = "default")] QueueMessage message)
        {
            _logger.LogInformation("C# Queue trigger function processed a request.");

            // Deserialize message back into a product.
            Order? order= JsonConvert.DeserializeObject<Order>(message.Body.ToString());
            
            // If product not null, add to database.
            if (order != null) 
            {
                _logger.LogInformation("Ordered product is added to CosmosDB");

                // Create and add to table
                await _orderService.CreateTable();
                await _orderService.InsertRecordToTable(order);
            }
        }
    }
}
