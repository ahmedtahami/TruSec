using Microsoft.AspNetCore.SignalR;
using TruSec.Backend.Interfaces;

namespace TruSec.Backend.Hubs
{
    public class ExpressionHub : Hub<IExpressionsHub>
    {
        public async Task SendExpression(string message)
        {
            await Clients.All.SendExpression(message);
        }
    }
}
