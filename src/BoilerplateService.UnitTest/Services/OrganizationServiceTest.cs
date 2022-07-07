

namespace BoilerplateService.UnitTest.Services
{
    public class OrganizationServiceTest
    {
        private readonly IOrganizationRepository _orgRepository;
        private readonly IOrganizationService _service;
        private readonly IFixture _fixture;
        private readonly IMapper _mapper;

        public OrganizationServiceTest()
        {
            _orgRepository = Substitute.For<IOrganizationRepository>();
            _fixture = FixtureFactory.GetInstance();
            _mapper = new MapperConfiguration(cfg => { cfg.AddProfile<AutoMapperProfile>(); })
                .CreateMapper();
            _service = new OrganizationService(_orgRepository, _mapper);
        }



        [Fact]
        //MethodName_StateUnderTest_ExpectedBehavior
        public async Task GetByIdAsync_ExistedId_ReturnOneOrganization()
        {
            // Given
            var expectedResult = _fixture.Create<Organization>();
            _orgRepository.GetByIdAsync(expectedResult.UUID).Returns(expectedResult);

            // When
            var result = await _service.GetByIdAsync(expectedResult.UUID);

            // Then
            // result.Should().BeEquivalentTo(expectedResult);
        }
    }
}