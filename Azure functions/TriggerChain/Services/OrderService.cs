﻿using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Configuration;
using TriggerChain.Models;
using TriggerChain.Services.Interfaces;

namespace TriggerChain.Services
{
    public class OrderService : ITableStorageService<Order>
    {
        private CloudTable _table;
        
        public OrderService()
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse("AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;DefaultEndpointsProtocol=http;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;");
            CloudTableClient tableClient = cloudStorageAccount.CreateCloudTableClient();
            _table = tableClient.GetTableReference("orders");
        }

        public async Task CreateTable()
        {
            try
            {
                var result = await _table.CreateIfNotExistsAsync();
            }
            catch (StorageException ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }

        public async Task InsertRecordToTable(Order order)
        {
            try
            {
                TableOperation tableOperation = TableOperation.Insert(order);
                TableResult result = await _table.ExecuteAsync(tableOperation);
                Order? insertedCustomer = result.Result as Order;
            }
            catch (StorageException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task<Order?> RetrieveRecord(string partitionKey, string rowKey)
        {
            TableOperation tableOperation = TableOperation.Retrieve<Order>(partitionKey, rowKey);
            TableResult tableResult = await _table.ExecuteAsync(tableOperation);
            return tableResult.Result as Order;
        }

        public List<Order> RetrieveAllAsync()
        {
            List<Order> orders = new List<Order>();

            TableQuery<Order> query = new TableQuery<Order>()
                   .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "1"));

            foreach (Order customer in _table.ExecuteQuerySegmentedAsync(query, null).Result)
            {
                orders.Add(customer);
            }
            return orders;
        }
    }
}
