using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriggerChain.Services
{
    public interface IQueueService
    {
        Task SendQueueMessageAsync(string queueName, object message);
    }
}
