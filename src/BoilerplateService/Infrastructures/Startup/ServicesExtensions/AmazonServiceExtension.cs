using Microsoft.Extensions.Options;

namespace BoilerplateService.Infrastructures.Startup.ServicesExtensions
{
    public static class AmazonServiceExtension
    {
        public static void AddDynamoDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            // Get the AWS profile information from configuration boilerplate
            var awsOptions = configuration.GetAWSOptions();

            // Configure AWS service clients to use these credentials
            services.AddDefaultAWSOptions(awsOptions);
            services.AddSingleton<IDynamoDBContext, DynamoDBContext>();

            var appSettings = services
                .BuildServiceProvider()
                .GetService<IOptions<AppSettings>>()
                .Value;

            if (appSettings.DynamoDb.LocalMode)
            {
                services.AddSingleton<IAmazonDynamoDB>(sp =>
                {
                    var clientConfig = new AmazonDynamoDBConfig
                    {
                        ServiceURL = appSettings.DynamoDb.LocalServiceUrl,
                    };
                    return new AmazonDynamoDBClient(clientConfig);
                });
            }
            else
            {
                services.AddAWSService<IAmazonDynamoDB>();
            }
        }
    }
}