using Microsoft.Extensions.Diagnostics.HealthChecks;
using BoilerplateService.Infrastructures.HealthChecks;

namespace BoilerplateService.Infrastructures.Startup.HealthCheckExtensions
{
    public static class DynamoDbHealthCheckExtensions
    {
        const string NAME = "dynamodb";

        public static IHealthChecksBuilder AddDynamoDB(this IHealthChecksBuilder builder, AppSettings appSettings, string name = default, HealthStatus? failureStatus = default, IEnumerable<string> tags = default, TimeSpan? timeout = default)
        {
            return builder.Add(new HealthCheckRegistration(
                name ?? NAME,
                sp => new DynamoHealth(appSettings),
                failureStatus,
                tags,
                timeout));
        }
    }
}