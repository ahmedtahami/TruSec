using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TruSec.BLL.DTOs;
using TruSec.BLL.Interfaces;

namespace TruSec.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TruckSecretsController : ControllerBase
    {
        private readonly ITruckSecretService _service;

        public TruckSecretsController(ITruckSecretService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var dto = await _service.GetAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _service.GetAsync();
            return Ok(result);
        }
        [HttpGet("GetByTruck/{truckId}")]
        public async Task<IActionResult> GetByTruck(int truckId)
        {
            var result = await _service.GetByTruckAsync(truckId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] TruckSecretDto dto)
        {
            await _service.AddAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TruckSecretDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            await _service.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
