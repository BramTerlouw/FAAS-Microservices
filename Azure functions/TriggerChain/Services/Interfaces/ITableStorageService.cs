using Microsoft.WindowsAzure.Storage.Table;
using TriggerChain.Models;

namespace TriggerChain.Services.Interfaces
{
    public interface ITableStorageService<T> where T : class
    {
        Task CreateTable();
        Task InsertRecordToTable(T order);
        Task<Order?> RetrieveRecord(string partitionKey, string rowKey);
        List<Order> RetrieveAllAsync();
    }
}
