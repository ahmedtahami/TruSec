using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TruSec.Backend.Hubs;
using TruSec.Backend.Interfaces;

namespace TruSec.Backend.Controllers
{
    [Obsolete]
    [ApiController]
    [Route("api/[controller]")]
    public class FacialExpressionController : ControllerBase
    {
        private readonly IHubContext<ExpressionHub> _hubContext;

        public FacialExpressionController(IHubContext<ExpressionHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ExpressionData data)
        {
            await _hubContext.Clients.Group(data.TruckId.ToString()).SendAsync("SendExpression", data);
            return Ok();
        }
    }
    [Obsolete]
    public class ExpressionData
    {
        public int TruckId { get; set; }
        public string Expression { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}