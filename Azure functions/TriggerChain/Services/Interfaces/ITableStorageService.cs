using Microsoft.WindowsAzure.Storage.Table;
using TriggerChain.Models;

namespace TriggerChain.Services.Interfaces
{
    public interface ITableStorageService<T> where T : class
    {
        Task<CloudTable> GetTableReference(string tableName);
        Task InsertRecordToTable(CloudTable cloudTable, T order);
        Task<Order?> RetrieveRecord(CloudTable cloudTable, string partitionKey, string rowKey);
        Task UpdateRecordInTable(CloudTable cloudTable);
        List<Order> RetrieveAll(CloudTable cloudTable);
    }
}
