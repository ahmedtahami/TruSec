using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TruSec.Backend.Hubs;

namespace TruSec.Backend.Controllers
{
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
            await _hubContext.Clients.All.SendAsync("ReceiveExpression", data.Expression);
            return Ok();
        }
    }

    public class ExpressionData
    {
        public string Expression { get; set; }
    }
}