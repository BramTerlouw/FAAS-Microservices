namespace TriggerChain.Services.Interfaces
{
    public interface ICosmosDbService<T> where T : class
    {
        Task<IEnumerable<T>> GetMultipleAsync(string query);
        Task<T> GetAsync(string id);
        Task AddAsync(T item);
        Task UpdateAsync(string id, T UpdateItem);
        Task DeleteAsync(string id);
    }
}
