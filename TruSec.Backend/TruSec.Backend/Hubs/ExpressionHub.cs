using Microsoft.AspNetCore.SignalR;
using TruSec.Backend.Controllers;
using TruSec.Backend.Interfaces;
using TruSec.BLL.DTOs;
using TruSec.DAL.Entities;

namespace TruSec.Backend.Hubs
{
    public class ExpressionHub : Hub<IExpressionsHub>
    {
        public async Task JoinTruckGroup(int truckId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, truckId.ToString());
        }

        public async Task LeaveTruckGroup(int truckId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, truckId.ToString());
        }

        public async Task SendExpression(TruckDataLogRequestDto truckData)
        {
            await Clients.Group(truckData.TruckId.ToString()).SendExpression(truckData);
        }
    }
}
