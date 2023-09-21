namespace TriggerChain.Services.Interfaces
{
    public interface IQueueService
    {
        Task SendQueueMessageAsync(string queueName, object message);
    }
}
