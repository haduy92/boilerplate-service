using BoilerplateService.Models.Dtos;
using BoilerplateService.Services.Interfaces;

namespace BoilerplateService.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _service;

        public OrganizationController(IOrganizationService service)
        {
            _service = service;
        }

        /// <summary>
        /// Create table organization.
        /// </summary>
        /// <response code="200">Created an organization table successfully</response>
        /// <response code="401">Authentication is failed</response>
        /// <response code="403">Authorization is failed</response>
        [HttpPost("create")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateTableAsync()
        {
            await _service.CreateTableAsync();
            return Ok();
        }

        /// <summary>
        /// Get an organization.
        /// </summary>
        /// <response code="200">Got an organization entity successfully</response>
        /// <response code="401">Authentication is failed</response>
        /// <response code="403">Authorization is failed</response>
        /// <response code="404">UUID is not existed</response>
        [HttpGet("{uuid}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(GetOrganizationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAsync(string uuid)
        {
            var entity = await _service.GetByIdAsync(uuid);
            return Ok(entity);
        }

        /// <summary>
        /// Create an organization.
        /// </summary>
        /// <response code="200">Created an organization entity successfully</response>
        /// <response code="401">Authentication is failed</response>
        /// <response code="403">Authorization is failed</response>
        [HttpPost()]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsync(CreateOrganizationDto dto)
        {
            var uuid = await _service.CreateAsync(dto);
            return Ok(uuid);
        }

        /// <summary>
        /// Update an organization.
        /// </summary>
        /// <response code="200">Modified an organization entity successfully</response>
        /// <response code="401">Authentication is failed</response>
        /// <response code="403">Authorization is failed</response>
        /// <response code="404">UUID is not existed</response>
        [HttpPut()]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync(UpdateOrganizationDto dto)
        {
            try
            {
                await _service.UpdateAsync(dto);
                return Ok();
            }
            catch (ResourceNotFoundException)
            {
                return NotFound(dto.UUID);
            }
        }

        /// <summary>
        /// Delete an organization.
        /// </summary>
        /// <response code="200">Deleted an organization entity successfully</response>
        /// <response code="401">Authentication is failed</response>
        /// <response code="403">Authorization is failed</response>
        [HttpDelete("{uuid}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteByIdAsync(string uuid)
        {
            await _service.DeleteByIdAsync(uuid);
            return Ok();
        }
    }
}