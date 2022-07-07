namespace BoilerplateService.Infrastructures.Contexts.Interfaces
{
    public interface IDynamoDatabaseContext
    {
        Task CreateTableAsync(CreateTableRequest createTableRequest);
        Task<PutItemResponse> PutItemAsync(PutItemRequest putItemRequest);
    }
}