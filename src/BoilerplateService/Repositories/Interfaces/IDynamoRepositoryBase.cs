namespace BoilerplateService.Repositories.Interfaces
{
    public interface IDynamoRepositoryBase<T> : IDisposable where T : class
    {
        Task CreateTableAsync();
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(string uuid);
        Task SaveAsync(T item);
        Task DeleteByIdAsync(string uuid);
    }
}