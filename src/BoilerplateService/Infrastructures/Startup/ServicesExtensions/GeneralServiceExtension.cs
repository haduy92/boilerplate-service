using Microsoft.AspNetCore.Mvc.Versioning;
using BoilerplateService.Infrastructures.Startup.HealthCheckExtensions;

namespace BoilerplateService.Infrastructures.Startup.ServicesExtensions
{
    public static class GeneralServiceExtension
    {
        public static void AddGeneralConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<AppSettings>(configuration);
            services.AddEndpointsApiExplorer();
            services.AddAutoMapper(typeof(BoilerplateService.AppSettings));

            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                     options.SerializerSettings.Converters.Add(new StringEnumConverter())
                );
            services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                o.ReportApiVersions = true;
                o.ApiVersionReader = ApiVersionReader.Combine(
                    new QueryStringApiVersionReader("api-version"),
                    new HeaderApiVersionReader("X-Version"),
                    new MediaTypeApiVersionReader("ver"));
            });
            services
                .AddHealthChecks()
                .AddDynamoDB(configuration.Get<AppSettings>()); ;
        }
    }
}