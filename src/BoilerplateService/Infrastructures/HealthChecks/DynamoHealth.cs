using Amazon.Runtime;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BoilerplateService.Infrastructures.HealthChecks
{
    public class DynamoHealth : IHealthCheck
    {
        private readonly AppSettings _appSettings;

        public DynamoHealth(AppSettings appSettings)
        {
            _appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                AmazonDynamoDBClient client;
                var clientConfig = new AmazonDynamoDBConfig();

                if (_appSettings.DynamoDb.LocalMode)
                {
                    clientConfig.ServiceURL = _appSettings.DynamoDb.LocalServiceUrl;
                    client = new AmazonDynamoDBClient(clientConfig);
                }
                else
                {
                    var credentials = new BasicAWSCredentials(_appSettings.AWS.AccessKey, _appSettings.AWS.SecretKey);
                    // clientConfig.AuthenticationRegion = _options.AuthenticationRegion;
                    // clientConfig.ServiceURL = _appSettings.AWS.ConnectionString;
                    client = new AmazonDynamoDBClient(credentials, clientConfig);
                }

                await client.ListTablesAsync(new ListTablesRequest { Limit = 1 }, cancellationToken);
                return HealthCheckResult.Healthy();
            }
            catch (Exception ex)
            {
                return new HealthCheckResult(context.Registration.FailureStatus, exception: ex);
            }
        }
    }
}