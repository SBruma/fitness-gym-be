using FitnessGym.Application.Dtos.Gyms;
using FitnessGym.Application.Services.Interfaces.Gyms;
using FitnessGym.Domain.Entities.Gyms;
using Microsoft.AspNetCore.Mvc;

namespace FitnessGym.API.Controllers.Gyms
{
    [Route("api/[controller]")]
    public class GymController : ControllerBase
    {
        private readonly IGymService _gymService;

        public GymController(IGymService gymService)
        {
            _gymService = gymService;
        }

        // GET: api/<GymController>
        [HttpGet]
        [ProducesResponseType(typeof(List<GymDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get()
        {
            var result = await _gymService.GetAll();

            return result.IsSuccess ? Ok(result.Value) : NotFound();
        }

        // GET api/<GymController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<GymDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _gymService.Get(new GymId(id));

            return result.IsSuccess ? Ok(result.Value) : NotFound();
        }

        // PUT api/<GymController>
        [HttpPost]
        [ProducesResponseType(typeof(GymDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Insert([FromBody] CreateGymDto request)
        {
            var result = await _gymService.Create(request);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
        }

        // DELETE api/<GymController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(List<GymDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _gymService.Delete(new GymId(id));

            return result.IsSuccess ? Ok() : NotFound();
        }

        // PUT api/<GymController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(List<GymDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateGymDto updateGymDto)
        {
            var result = await _gymService.Update(new GymId(id), updateGymDto);

            return result.IsSuccess ? Ok(result.Value) : NotFound();
        }
    }
}
