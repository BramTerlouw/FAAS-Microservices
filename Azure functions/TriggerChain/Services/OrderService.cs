using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Configuration;
using TriggerChain.Models;
using TriggerChain.Services.Interfaces;

namespace TriggerChain.Services
{
    public class OrderService : ITableStorageService<Order>
    {
        private readonly CloudTableClient _tableClient;
        
        public OrderService()
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["connString"]);
            _tableClient = cloudStorageAccount.CreateCloudTableClient();
        }

        public async Task<CloudTable> GetTableReference(string tableName)
        {
            CloudTable cloudTable = _tableClient.GetTableReference(tableName);
            try
            {
                var result = await cloudTable.CreateIfNotExistsAsync();
            }
            catch (StorageException ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            return cloudTable;
        }

        public async Task InsertRecordToTable(CloudTable cloudTable, Order order)
        {
            try
            {
                TableOperation tableOperation = TableOperation.Insert(order);
                TableResult result = await cloudTable.ExecuteAsync(tableOperation);
                Order? insertedCustomer = result.Result as Order;
            }
            catch (StorageException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task<Order?> RetrieveRecord(CloudTable cloudTable, string partitionKey, string rowKey)
        {
            TableOperation tableOperation = TableOperation.Retrieve<Order>(partitionKey, rowKey);
            TableResult tableResult = await cloudTable.ExecuteAsync(tableOperation);
            return tableResult.Result as Order;
        }

        public async Task UpdateRecordInTable(CloudTable cloudTable)
        {
            string orderID = "1";
            string orderName = "Nvidia rtx 4060";

            Order? orderEntity = await RetrieveRecord(cloudTable, orderID, orderID + orderName);
            if (orderEntity is not null)
            {
                orderEntity.OrderName = "Nvidia rtx 4060 UPDATED";
                TableOperation tableOperation = TableOperation.Replace(orderEntity);
                var result = await cloudTable.ExecuteAsync(tableOperation);
            }
        }

        public List<Order> RetrieveAll(CloudTable cloudTable)
        {
            List<Order> orders = new List<Order>();

            TableQuery<Order> query = new TableQuery<Order>()
                   .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "1"));

            foreach (Order customer in cloudTable.ExecuteQuerySegmentedAsync(query, null).Result)
            {
                orders.Add(customer);
            }
            return orders;
        }
    }
}
