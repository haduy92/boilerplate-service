using BoilerplateService.Infrastructures.Contexts.Interfaces;

namespace BoilerplateService.Infrastructures.Contexts
{
    public class DynamoDatabaseContext : IDynamoDatabaseContext
    {
        private const string StatusUnknown = "UNKNOWN";
        private const string StatusActive = "ACTIVE";

        private readonly IAmazonDynamoDB _client;

        public DynamoDatabaseContext(IAmazonDynamoDB client)
        {
            _client = client;
        }

        public async Task CreateTableAsync(CreateTableRequest createTableRequest)
        {
            var status = await GetTableStatusAsync(createTableRequest.TableName);

            if (!status.EqualsIgnoreCase(StatusUnknown))
            {
                return;
            }

            createTableRequest.ProvisionedThroughput = new ProvisionedThroughput()
            {
                ReadCapacityUnits = 5,
                WriteCapacityUnits = 5
            };

            await _client.CreateTableAsync(createTableRequest);
            await WaitUntilTableReady(createTableRequest.TableName);
        }

        public async Task<PutItemResponse> PutItemAsync(PutItemRequest putItemRequest)
        {
            return await _client.PutItemAsync(putItemRequest);
        }

        private async Task<string> GetTableStatusAsync(string tableName)
        {
            try
            {
                var response = await _client.DescribeTableAsync(new DescribeTableRequest
                {
                    TableName = tableName
                });
                return response?.Table.TableStatus;
            }
            catch (ResourceNotFoundException)
            {
                return StatusUnknown;
            }
        }

        private async Task WaitUntilTableReady(string tableName)
        {
            var status = await GetTableStatusAsync(tableName);
            for (var i = 0; i < 10 && status != StatusActive; ++i)
            {
                await Task.Delay(500);
                status = await GetTableStatusAsync(tableName);
            }
        }
    }
}