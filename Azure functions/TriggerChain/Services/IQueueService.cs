namespace TriggerChain.Services
{
    public interface IQueueService
    {
        Task SendQueueMessageAsync(string queueName, object message);
    }
}
