using AutoMapper;
using BoilerplateService.Models.Dtos;
using BoilerplateService.Models.Entities;

namespace BoilerplateService.Infrastructures.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateOrganizationDto, Organization>();
            CreateMap<UpdateOrganizationDto, Organization>();
            CreateMap<Organization, GetOrganizationDto>();
        }
    }
}