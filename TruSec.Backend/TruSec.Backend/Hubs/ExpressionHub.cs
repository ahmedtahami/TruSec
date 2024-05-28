using Microsoft.AspNetCore.SignalR;
using TruSec.Backend.Controllers;
using TruSec.Backend.Interfaces;

namespace TruSec.Backend.Hubs
{
    public class ExpressionHub : Hub<IExpressionsHub>
    {
        public async Task SendExpression(ExpressionData data)
        {
            await Clients.All.SendExpression(data);
        }
    }
}
