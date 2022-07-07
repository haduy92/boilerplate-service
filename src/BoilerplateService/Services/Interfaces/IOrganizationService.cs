using BoilerplateService.Models.Dtos;

namespace BoilerplateService.Services.Interfaces
{
    public interface IOrganizationService
    {
        Task CreateTableAsync();
        Task<GetOrganizationDto> GetByIdAsync(string uuid);
        Task<string> CreateAsync(CreateOrganizationDto dto);
        Task UpdateAsync(UpdateOrganizationDto dto);
        Task DeleteByIdAsync(string uuid);
    }
}