using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TruSec.Backend.Hubs;
using TruSec.BLL.DTOs;
using TruSec.BLL.Interfaces;

namespace TruSec.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TruckDataLogsController : ControllerBase
    {
        private readonly ITruckDataLogService _service;
        private readonly IHubContext<ExpressionHub> _hubContext;
        public TruckDataLogsController(ITruckDataLogService service, IHubContext<ExpressionHub> hubContext)
        {
            _service = service;
            _hubContext = hubContext;
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
        public async Task<IActionResult> Add([FromBody] TruckDataLogRequestDto dto)
        {
            await _hubContext.Clients.Group(dto.TruckId.ToString()).SendAsync("SendExpression", dto);
            await _service.AddAsync(new TruckDataLogDto
            {
                TruckId = dto.TruckId,
                DriverExpression = dto.DriverExpression,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                SpeedKPH = dto.SpeedKPH,
                TimeStamp = dto.TimeStamp
            });
            return CreatedAtAction(nameof(Get), dto);
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
