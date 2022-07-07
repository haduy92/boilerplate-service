using AutoMapper;
using BoilerplateService.Models.Dtos;
using BoilerplateService.Models.Entities;
using BoilerplateService.Repositories.Interfaces;
using BoilerplateService.Services.Interfaces;

namespace BoilerplateService.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IOrganizationRepository _repository;
        private readonly IMapper _mapper;

        public OrganizationService(IOrganizationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateTableAsync()
        {
            await _repository.CreateTableAsync();
        }

        public async Task<GetOrganizationDto> GetByIdAsync(string uuid)
        {
            var entity = await _repository.GetByIdAsync(uuid);

            if (entity is null)
            {
                throw new ResourceNotFoundException($"{nameof(Organization)}:{uuid}");
            }

            return _mapper.Map<GetOrganizationDto>(entity);
        }

        public async Task<string> CreateAsync(CreateOrganizationDto dto)
        {
            var entity = _mapper.Map<Organization>(dto);
            await _repository.SaveAsync(entity);
            return entity.UUID;
        }

        public async Task UpdateAsync(UpdateOrganizationDto dto)
        {
            var existed = await _repository.GetByIdAsync(dto.UUID);

            if (existed is null)
            {
                throw new ResourceNotFoundException($"{nameof(Organization)}:{dto.UUID}");
            }

            var entity = _mapper.Map<Organization>(dto);
            await _repository.SaveAsync(entity);
        }

        public async Task DeleteByIdAsync(string uuid)
        {
            await _repository.DeleteByIdAsync(uuid);
        }
    }
}