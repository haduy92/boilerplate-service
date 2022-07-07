using BoilerplateService.Infrastructures.Contexts;
using BoilerplateService.Infrastructures.Contexts.Interfaces;
using BoilerplateService.Repositories;
using BoilerplateService.Repositories.Interfaces;
using BoilerplateService.Services;
using BoilerplateService.Services.Interfaces;

namespace BoilerplateService.Infrastructures.Startup.ServicesExtensions
{
    public static class InjectionServiceExtension
    {
        public static void AddInjectedServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDynamoDatabaseContext, DynamoDatabaseContext>();
            services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            services.AddScoped<IOrganizationService, OrganizationService>();
        }
    }
}