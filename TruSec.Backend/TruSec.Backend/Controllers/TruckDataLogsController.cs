using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TruSec.BLL.DTOs;
using TruSec.BLL.Interfaces;

namespace TruSec.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TruckDataLogsController : ControllerBase
    {
        private readonly ITruckDataLogService _service;

        public TruckDataLogsController(ITruckDataLogService service)
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

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] TruckDataLogDto dto)
        {
            await _service.AddAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TruckDataLogDto dto)
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
