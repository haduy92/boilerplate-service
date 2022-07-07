using BoilerplateService.Infrastructures.Contexts.Interfaces;
using BoilerplateService.Models.Entities.Base;
using BoilerplateService.Repositories.Interfaces;

namespace BoilerplateService.Repositories.Base
{
    public abstract class DynamoRepositoryBase<T> : DynamoDBContext, IDynamoRepositoryBase<T> where T : EntityBase
    {
        protected readonly IDynamoDatabaseContext _context;

        protected DynamoRepositoryBase(IAmazonDynamoDB client, IDynamoDatabaseContext context)
            : base(client)
        {
            _context = context;
        }

        public abstract Task CreateTableAsync();

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await base.ScanAsync<T>(new List<ScanCondition>()).GetRemainingAsync();
        }

        public async Task<T> GetByIdAsync(string uuid)
        {
            return await base.LoadAsync<T>(uuid);
        }

        public async Task SaveAsync(T item)
        {
            await base.SaveAsync<T>(item);
        }

        public async Task DeleteByIdAsync(string uuid)
        {
            await base.DeleteAsync<T>(uuid);
        }
    }
}