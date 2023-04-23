using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace XNVSU0_HFT_202231.Endpoint.Services
{
    public class SignalRHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            Clients.Caller.SendAsync("Connected", Context.ConnectionId);
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception ex)
        {
            Clients.Caller?.SendAsync("Disconnected", Context.ConnectionId);
            return base.OnDisconnectedAsync(ex);
        }
    }
}
