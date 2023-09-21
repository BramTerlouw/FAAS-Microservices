namespace TriggerChain.Services.Interfaces
{
    public interface ICosmosDbService<T> where T : class
    {
        Task<IEnumerable<T>> GetMultipleAsync(string query);
        Task<T> GetAsync(string id);
        Task AddAsync(T item);
    }
}
