﻿using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using TriggerChain.Services.Interfaces;

namespace TriggerChain.Services
{
    public class QueueService : IQueueService
    {

        private readonly CloudQueueClient _queueClient;

        public QueueService()
        {
            string connectionString = "UseDevelopmentStorage=true";
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            _queueClient = storageAccount.CreateCloudQueueClient();
        }

        public async Task SendQueueMessageAsync(string queueName, object message)
        {
            CloudQueue queue = _queueClient.GetQueueReference(queueName);
            await queue.CreateIfNotExistsAsync();

            string serializedMessage = JsonConvert.SerializeObject(message);
            CloudQueueMessage cloudMessage = new CloudQueueMessage(serializedMessage);

            await queue.AddMessageAsync(cloudMessage);
        }

    }
}
